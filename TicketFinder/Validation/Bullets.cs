using System;
using System.Collections.Generic;
using System.Drawing;

namespace TicketFinder.Validation
{
    /// <summary>
    /// An entity that can damage the human
    /// </summary>
    abstract class Bullet
    {
        /// <summary>
        /// Position
        /// </summary>
        protected float x, y;

        /// <summary>
        /// Change the position
        /// </summary>
        /// <param name="durationSinceLastTick">How many milliseconds have passed since the last time this was called?</param>
        internal abstract void RecalculatePosition(float durationSinceLastTick);

        /// <summary>
        /// Draw the bullet
        /// </summary>
        internal abstract void Animate(Graphics g);

        /// <summary>
        /// Is the bullet colliding with the human?
        /// </summary>
        internal virtual bool TouchingHuman(Fighter human)
        {
            return (human.x - x) * (human.x - x) + (human.y - y) * (human.y - y) < 32 * 32;
        }

        /// <summary>
        /// Has the bullet left the screen (and will not return)?
        /// </summary>
        internal abstract bool CanBeRemoved();

        protected Random random = new Random();
    }

    class LiterallyJustAGun : Bullet
    {
        public LiterallyJustAGun()
        {
            this.x = 400;
            this.y = 200;
        }

        double direction = 0;

        internal override void Animate(Graphics g)
        {
            g.DrawImageRotated(Properties.Resources._120px_Pistol_icon, x + 60 * (float)Math.Cos(direction), y + 60 * (float)Math.Sin(direction), direction);
        }

        internal override bool CanBeRemoved()
        {
            return false;
        }

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            if (direction < Math.PI / 2)
                direction += durationSinceLastTick * Math.PI / 1200;
        }
    }

    class LiterallyJustAGunsBullet : Bullet
    {
        public LiterallyJustAGunsBullet()
        {
            this.x = 420;
            this.y = 300;
        }

        internal override void Animate(Graphics g)
        {
            g.FillEllipse(Brushes.White, x - 10, y - 10, 20, 20);
        }

        internal override bool CanBeRemoved()
        {
            return y > 810;
        }

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            y += durationSinceLastTick / 4;
        }
    }

    class DroppingTable : Bullet
    {
        float dy = 1;
        double direction = 0;

        public DroppingTable(int x)
        {
            this.x = x;
            this.y = 200;
        }

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            y += durationSinceLastTick * dy / 20;
            dy += durationSinceLastTick / 200;
            direction += durationSinceLastTick / 200;
        }

        internal override bool CanBeRemoved()
        {
            return y > 650;
        }

        internal override void Animate(Graphics g)
        {
            g.DrawImageRotated(Properties.Resources.Table, x, y, direction);
        }
    }

    class Bit : Bullet
    {
        readonly int value;
        readonly float dx, dy;

        public Bit(int value, float startX, float startY, double direction)
        {
            this.value = value;
            this.x = startX;
            this.y = startY;
            this.dx = (float)Math.Cos(direction) * 10;
            this.dy = (float)Math.Sin(direction) * 10;
        }

        internal override bool CanBeRemoved()
        {
            return dx > 0 ? x > 820 : x < -20;
        }

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            x += dx * durationSinceLastTick / 20;
            y += dy * durationSinceLastTick / 20;
        }

        internal override void Animate(Graphics g)
        {
            g.DrawText(value.ToString(), new RectangleF(x - 16, y - 16, 40, 40), Brushes.White, new Font("Consolas", 32, FontStyle.Bold));
        }
    }

    class EntityInERD : Bullet
    {
        readonly Direction direction;
        readonly int speed;
        readonly string title;
        readonly string contents;

        public EntityInERD(Direction direction, int x, int y, int speed)
        {
            this.direction = direction;
            this.speed = speed;
            this.x = x;
            this.y = y;

            do title = titles[random.Next(titles.Length)];
            while (previousTitles.Contains(title));
            previousTitles.Add(title);
            if (previousTitles.Count > 8) previousTitles.RemoveAt(0);
            contents =
                "ID      Guid (PK)\n" +
                "Owner   Guid (FK)\n" +
                "Name    String\n" +
                "Code    String\n" +
                "Index   Integer\n";
        }

        static readonly string[] titles = new string[] { 
            "Object", "Item", "Entity", "Thing", 
            "Facet", "Piece", "Device", "Focus",
            "Thingy", "Resource", "Asset", "Supply"
        };
        static readonly List<string> previousTitles = new List<string>();

        internal override void Animate(Graphics g)
        {
            g.FillRectangle(Brushes.Black, x, y, 128, 128);
            g.FillRectangle(Brushes.White, x, y, 128, 30);
            g.FillRectangle(Brushes.White, x, y, 10, 128);
            g.FillRectangle(Brushes.White, x + 118, y, 10, 128);
            g.FillRectangle(Brushes.White, x, y + 118, 128, 10);
            g.DrawLine(Pens.White, x + 58, y + 30, x + 58, y + 118);
            g.DrawText(title, new RectangleF(x + 10, y, 110, 30), Brushes.Black, new Font("Consolas", 16));
            g.DrawText(contents, new RectangleF(x + 10, y + 40, 108, 88), Brushes.White, new Font("Consolas", 8));
        }

        internal override bool CanBeRemoved()
        {
            return x < -128 || x > 800 || y < -128 || y > 600;
        }

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            switch (direction)
            {
                case Direction.Right:
                    x += speed * durationSinceLastTick / 20;
                    break;
                case Direction.Left:
                    x -= speed * durationSinceLastTick / 20;
                    break;
                case Direction.Down:
                    y += speed * durationSinceLastTick / 20;
                    break;
                case Direction.Up:
                    y -= speed * durationSinceLastTick / 20;
                    break;
            }
        }

        internal override bool TouchingHuman(Fighter human)
        {
            return
                x < human.x + 12 &&
                x + 128 > human.x - 12 &&
                y < human.y + 12 &&
                y + 128 > human.y - 12;
        }
    }

    enum Direction { Up, Down, Left, Right }

    class RedErrorCell : Bullet
    {
        public RedErrorCell(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        float aliveTime = 0;
        readonly int width;
        readonly int height;

        internal override void RecalculatePosition(float durationSinceLastTick)
        {
            aliveTime += durationSinceLastTick;
        }

        internal override void Animate(Graphics g)
        {
            if (aliveTime > 1000)
            {
                g.FillRectangle(Brushes.White, x, y, width, height);
            }
            g.FillRectangle(Brushes.Red, x, y, 2, height);
            g.FillRectangle(Brushes.Red, x, y, width, 2);
            g.FillRectangle(Brushes.Red, x + width - 2, y, 2, height);
            g.FillRectangle(Brushes.Red, x, y + height - 2, width, 2);
        }

        internal override bool CanBeRemoved()
        {
            return aliveTime >= 2000;
        }

        internal override bool TouchingHuman(Fighter human)
        {
            return aliveTime > 1000 &&
                x < human.x + 12 &&
                x + width > human.x - 12 &&
                y < human.y + 12 &&
                y + height > human.y - 12;
        }
    }

}
