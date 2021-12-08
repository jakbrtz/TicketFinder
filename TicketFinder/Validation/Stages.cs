using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicketFinder.Validation
{
    /// <summary>
    /// A short sequence of events
    /// </summary>
    abstract class Stage
    {
        /// <summary>
        /// How long in milliseconds will this stage last
        /// </summary>
        private readonly int duration;

        public Stage(int duration)
        {
            this.duration = duration;
        }

        /// <summary>
        /// How long in milliseconds has this stage lasted for
        /// </summary>
        protected float aliveTime = 0;

        protected Random random = new Random();

        /// <summary>
        /// How long in milliseconds has passed since the last time the <seealso cref="Tick"/> method run?
        /// </summary>
        private float mostRecentTickDuration;

        /// <summary>
        /// Update the stage
        /// </summary>
        public void Tick(Fighter human, float duractionSinceLastTick)
        {
            aliveTime += duractionSinceLastTick;
            mostRecentTickDuration = duractionSinceLastTick;
            TickExtension(human, duractionSinceLastTick);
        }

        /// <summary>
        /// Has a timestamp passed since the last time we called <seealso cref="Tick"/>?
        /// For example, if the last Tick was at 292.2 and this tick is at 301.8 then the last tick contains the timestamp '300'
        /// </summary>
        /// <param name="timestamp">The timestamp that we are interested in</param>
        /// <param name="durationSinceTimestamp">The duration since that timestamp has passed</param>
        protected bool LastTickContained(float timestamp, out float durationSinceTimestamp)
        {
            durationSinceTimestamp = aliveTime - timestamp;
            return durationSinceTimestamp > 0 && durationSinceTimestamp <= mostRecentTickDuration;
        }

        /// <summary>
        /// Has a recurring timestamp passed since the last time we called <seealso cref="Tick"/>?
        /// For example, if the last Tick was at 292.2 and this tick is at 301.8 then the last tick contains the timestamp '300'
        /// This is useful if we want an event to happen every 100 milliseconds
        /// </summary>
        /// <param name="timestampMod">The period of the timestamp that we are interested in</param>
        /// <param name="durationSinceTimestamp">The duration since that timestamp has passed</param>
        /// <param name="timestampOffset">The remainder when dividing the timestamp by timestampMod</param>
        protected bool LastTickContainedMod(float timestampMod, out float durationSinceTimestamp, float timestampOffset = 0)
        {
            durationSinceTimestamp = (aliveTime - timestampOffset) % timestampMod;
            return durationSinceTimestamp > 0 && durationSinceTimestamp <= mostRecentTickDuration;
        }

        /// <summary>
        /// Additional actions that need to happen every tick
        /// </summary>
        public virtual void TickExtension(Fighter human, float duractionSinceLastTick) { }

        /// <summary>
        /// Draw the stage
        /// </summary>
        public abstract void Animate(Graphics g);

        /// <summary>
        /// Is this stage complete?
        /// </summary>
        public virtual bool Finished()
        {
            return aliveTime >= duration;
        }
    }

    /// <summary>
    /// A stage that does nothing but wait for a set duration
    /// </summary>
    class EmptyStage : Stage
    {
        public EmptyStage(int duration) : base(duration)
        {
        }

        public override void Animate(Graphics g) { }
    }

    /// <summary>
    /// A stage where a speech bubble appears to tell the human something
    /// </summary>
    class Dialogue : Stage
    {
        public Dialogue(string str, int duration) : base(duration)
        {
            this.text = str;
        }

        readonly string text;

        public override void Animate(Graphics g)
        {
            g.DrawImageRotated(Properties.Resources.speech_bubble, 480, 75);
            g.DrawText(text, new RectangleF(390, 50, 200, 60), Brushes.Black);
        }
    }

    /// <summary>
    /// A stage where bullets are sent to damage the human
    /// </summary>
    abstract class AttackPattern : Stage
    {
        public AttackPattern(int duration) : base(duration) { }

        /// <summary>
        /// A collection of bullets that are active. 
        /// This list updates during the stage as bullets are added and removed.
        /// </summary>
        protected List<Bullet> bullets = new List<Bullet>();

        /// <summary>
        /// Add a bullet to this stage
        /// </summary>
        /// <param name="bullet">The bullet being added</param>
        /// <param name="age">The duration since the timestamp when the bullet was meant to be added</param>
        protected void AddBullet(Bullet bullet, float age)
        {
            bullets.Add(bullet);
            bullet.RecalculatePosition(age);
        }

        /// <summary>
        /// Update all the bullets
        /// </summary>
        public override void TickExtension(Fighter human, float durationSinceLastTick)
        {
            // Move existing bullets
            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.RecalculatePosition(durationSinceLastTick);
            }
            // Remove bullets that aren't needed anymore
            bullets.RemoveAll(b => b.CanBeRemoved());
            // Add more bullets if necessary
            SummonBullets(human);
            // Damage the human
            if (human.remainingInvulnerabilityTime <= 0 && TouchingHuman(human))
            {
                human.remainingInvulnerabilityTime = 500;
                human.hp--;
            }
        }

        /// <summary>
        /// Create more bullets for this stage
        /// </summary>
        public abstract void SummonBullets(Fighter human);

        /// <summary>
        /// Check if the human should take damage
        /// </summary>
        public virtual bool TouchingHuman(Fighter human)
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.TouchingHuman(human))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Animate(Graphics g)
        {
            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.Animate(g);
            }
        }
    }

    class LiterallyAGun : AttackPattern
    {
        public LiterallyAGun() : base(2000) { }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContained(0, out float durationSinceGun))
            {
                AddBullet(new LiterallyJustAGun(), durationSinceGun);
            }

            if (LastTickContained(600, out float durationSinceBullet))
            {
                AddBullet(new LiterallyJustAGunsBullet(), durationSinceBullet);
            }
        }
    }

    class DropTables : AttackPattern
    {
        public DropTables(int duration) : base(duration) { }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(160, out float durationSinceTimestamp))
            {
                AddBullet(new DroppingTable(new Random().Next(200, 600)), durationSinceTimestamp);
            }
        }
    }

    class Bytes : AttackPattern
    {
        public Bytes(int duration) : base(duration) { }

        float startX, startY, direction;

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(800, out _))
            {
                startX = random.Next(2) * 800;
                startY = random.Next(330, 470);
                direction = (float)random.NextDouble() / 10 - 0.05f;
                if (startX != 0) direction += (float)Math.PI;
            }

            for (int i = 0; i < 8; i++)
            {
                if (LastTickContainedMod(800, out float durationSinceTimestamp, i * 80))
                {
                    bool bitValue = random.Next(2) == 0;
                    if (aliveTime / 800 < message.Length)
                    {
                        char c = message[(int)(aliveTime / 800)];
                        int index = startX == 0 ? i : 7 - i;
                        bitValue = (c & (1 << index)) != 0;
                    }
                    AddBullet(new Bit(bitValue ? 1 : 0, startX, startY, direction), durationSinceTimestamp);
                }
            }
        }

        readonly string message = "get rekt";
    }

    class ERDiagram1 : AttackPattern
    {
        public ERDiagram1(int duration) : base(duration) { }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(1500, out float durationSinceTimestamp))
            {
                AddBullet(new EntityInERD(Direction.Left, 800, LastTickContainedMod(3000, out _) ? 276 : 396, 3), durationSinceTimestamp);
            }
            if (LastTickContainedMod(2200, out durationSinceTimestamp))
            {
                AddBullet(new EntityInERD(Direction.Down, random.Next(200, 472), -128, 4), durationSinceTimestamp);
            }
        }
    }

    class ERDiagram2 : AttackPattern
    {
        public ERDiagram2(int duration) : base(duration) { }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(2200, out float durationSinceTimestamp))
            {
                AddBullet(new EntityInERD(Direction.Left, 786, 266, 2), durationSinceTimestamp);
                AddBullet(new EntityInERD(Direction.Right, -128, 406, 2), durationSinceTimestamp);
            }
        }
    }

    class RedErrorTable : AttackPattern
    {
        readonly int rows;
        readonly int cols;

        public RedErrorTable(int rows, int cols, int duration) : base(duration) 
        {
            this.rows = rows;
            this.cols = cols;
        }

        public override void Animate(Graphics g)
        {
            for (int i = 1; i < rows; i++)
                g.DrawLine(Pens.Gray, 200, 300 + (i * 200 / rows), 600, 300 + (i * 200 / rows));
            for (int i = 1; i < cols; i++)
                g.DrawLine(Pens.Gray, 200 + (i * 400 / cols), 300, 200 + (i * 400 / cols), 500);
            base.Animate(g);
        }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(250, out float durationSinceTimestamp))
            {
                int width = 400 / cols;
                int height = 200 / rows;
                int xi = random.Next(cols);
                int yi = random.Next(rows);
                if (LastTickContainedMod(1500, out _) && !LastTickContained(0, out _))
                {
                    xi = (int)((human.x - 200) / width);
                    yi = (int)((human.y - 300) / height);
                }
                int x = 200 + xi * width;
                int y = 300 + yi * height;
                AddBullet(new RedErrorCell(x, y, 400 / cols, 200 / rows), durationSinceTimestamp);
            }
        }
    }

    class WildBits : AttackPattern
    {
        public WildBits(int duration) : base(duration) { }

        public override void SummonBullets(Fighter human)
        {
            if (LastTickContainedMod(400, out float durationSinceTimestamp))
            {
                float targetX = random.Next(250, 550);
                float targetY = random.Next(320, 480);
                double direction = random.NextDouble() * 2 * Math.PI;
                float dx = (float)Math.Cos(direction);
                float dy = (float)Math.Sin(direction);
                while (targetX > -50 && targetX < 850 && targetY > -50 && targetY < 650)
                {
                    targetX -= dx;
                    targetY -= dy;
                }
                AddBullet(new Bit(LastTickContained(4800, out _) ? 2 
                    : random.Next(2), targetX, targetY, direction), 
                    durationSinceTimestamp);
            }
        }
    }
}
