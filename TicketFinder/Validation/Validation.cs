using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TicketFinder.Validation
{
    public partial class Validation : Form
    {
        public Validation()
        {
            InitializeComponent();
        }

        private void Validation2_Load(object sender, EventArgs e)
        {
            SoundPlayerAsync.PlaySound(Properties.Resources.music);
        }


        readonly Fighter human = new Fighter() { x = 400, y = 400, hp = 20 };
        int stageIndex = 0;
        readonly Stage[] stages = new Stage[] {
            new EmptyStage(1000),
            new Dialogue("I can't handle incompetent users like yourself", 4000),
            new LiterallyAGun(),
            new Dialogue("Since you can't follow the rules I'll confiscate your database", 4000),
            new Bytes(8000),
            new Dialogue("Now I've got to drop tables", 2000),
            new DropTables(10000),
            new Dialogue("People like you are why we need to refactor everything", 3000),
            new ERDiagram1(12000),
            new Dialogue("Why is 'Unique Identifier' so difficult for you to grasp?", 3000),
            new ERDiagram2(14000),
            new Dialogue("You need a kick in the back end", 2000),
            new Bytes(14000),
            new Dialogue("When you see an error border you should try avoid it", 3000),
            new RedErrorTable(5, 4, 12000),
            new Dialogue("We reject your data because it makes no logical sense", 4000),
            new Dialogue("or because we're too lazy to process it", 1000),
            new WildBits(12000),
            new Dialogue("How the tables have turned", 2000),
            new DropTables(10000),
            new Dialogue("This battle is nearly over", 2000),
            new ERDiagram1(14000),
            new Dialogue("You fought well \nYou can keep your data", 5000),
        };

        DateTime lastTick = DateTime.Now;
        int postDeathTime = 0;

        private void Tick(object sender, EventArgs e)
        {
            DateTime thisTick = DateTime.Now;
            float durationSinceLastTick = (float)((thisTick - lastTick).TotalMilliseconds);
            lastTick = thisTick;

            if (human.hp > 0)
            {
                MoveEverything(durationSinceLastTick);
            }
            else
            {
                postDeathTime++;
                if (postDeathTime == 100)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }

            Animate();

        }

        bool movingUp, movingDown, movingLeft, movingRight;
        bool userHasMoved = false;

        private void Validation2_KeyDown(object sender, KeyEventArgs e)
        {
            HandlePress(e.KeyCode, true);
        }

        private void Validation2_KeyUp(object sender, KeyEventArgs e)
        {
            HandlePress(e.KeyCode, false);
        }

        void HandlePress(Keys key, bool pressed)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.W:
                    movingUp = pressed;
                    break;
                case Keys.Down:
                case Keys.S:
                    movingDown = pressed;
                    break;
                case Keys.Left:
                case Keys.A:
                    movingLeft = pressed;
                    break;
                case Keys.Right:
                case Keys.D:
                    movingRight = pressed;
                    break;
            }

            userHasMoved |= movingUp || movingDown || movingLeft || movingRight;
        }

        void MoveEverything(float durationSinceLastTick)
        {
            float humanSpeed = durationSinceLastTick / 10;

            if (movingUp && human.y - 16 > 300) human.y-= humanSpeed;
            if (movingDown && human.y + 16 < 500) human.y+= humanSpeed;
            if (movingLeft && human.x - 16 > 200) human.x-= humanSpeed;
            if (movingRight && human.x + 16 < 600) human.x+= humanSpeed;
            if (stageIndex < stages.Length)
            {
                stages[stageIndex].Tick(human, durationSinceLastTick);
            }
            if (human.remainingInvulnerabilityTime > 0)
            {
                human.remainingInvulnerabilityTime-=durationSinceLastTick;
            }
            if (stageIndex < stages.Length && stages[stageIndex].Finished())
            {
                stageIndex++;
                if (stageIndex == stages.Length)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        void Animate()
        {
            GC.Collect();
            Bitmap bmp = new Bitmap(800, 600);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Refresh the screen
                g.Clear(Color.Black);
                // Add a bounding box for the human
                g.DrawRectangle(Pens.White, 200, 300, 400, 200);
                // Animate the current stage
                if (stageIndex < stages.Length)
                {
                    stages[stageIndex].Animate(g);
                }
                // If the human's hp has been depleted, animate a stage where I taunt the human
                if (human.hp <= 0)
                {
                    new Dialogue("No so tough now \nSay goodbye to your data", 1000).Animate(g);
                }
                // Draw me
                g.DrawImageRotated(Properties.Resources.Clippy, 400, 150);
                // If the human was recently hit, draw a red circle around it
                if (human.remainingInvulnerabilityTime > 400)
                {
                    g.FillEllipse(Brushes.Red, human.x - 20, human.y - 20, 40, 40);
                }
                // Draw the human
                g.DrawImageRotated(Properties.Resources.confluence_logo_D9B07137C2_seeklogo_com, human.x, human.y);
                // Report the human's remaining hp
                g.DrawText($"HP: {human.hp:D2}/20", new RectangleF(340, 500, 120, 24), Brushes.White, new Font("Consolas", 16));
                // If the human hasn't moved then tell it that it can move
                if (!userHasMoved && stageIndex >= 2)
                {
                    g.DrawText($"arrow keys to dodge", new RectangleF(279, 540, 242, 24), Brushes.White, new Font("Consolas", 16));
                }
            }
            this.BackgroundImage = bmp;
        }
    }

    static class GraphicExtension
    {
        public static void DrawImageRotated(this Graphics g, Image image, float x, float y, double angle = 0)
        {
            if (angle == 0)
            {
                g.DrawImage(image, x - image.Width / 2, y - image.Height / 2);
                return;
            }

            void Rotate(float initialX, float initialY, out int replacementX, out int replacementY)
            {
                replacementX = (int)(x + Math.Cos(angle) * initialX - Math.Sin(angle) * initialY);
                replacementY = (int)(y + Math.Sin(angle) * initialX + Math.Cos(angle) * initialY);
            }

            Rotate(-image.Width / 2, -image.Height / 2, out int x1, out int y1);
            Rotate( image.Width / 2, -image.Height / 2, out int x2, out int y2);
            Rotate(-image.Width / 2,  image.Height / 2, out int x3, out int y3);

            g.DrawImage(image, new Point[] {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y3),
            });
        }

        public static void DrawText(this Graphics g, string text, RectangleF boundingBox, Brush colour, Font font = null)
        {
            g.DrawString(text, font ?? new Font("Tahoma", 10), colour, boundingBox);
        }
    }

    class Fighter
    {
        internal float x;
        internal float y;
        internal int hp;
        internal float remainingInvulnerabilityTime = 0;
    }
}
