using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace five_in_line
{
    public partial class frmFive : Form
    {
        const int intBlock = 27; // 格子宽度
        const int intSize = 15;  // 棋盘大小

        int[,] board; // 0=无 1=黑 2=白
        int intStatus; // 0=未开始 1=黑 2=白 3=黑胜 4=白胜

        struct PointEx {
            public int x;
            public int y;
            public int state;
        }

        Stack<PointEx> regret = new Stack<PointEx>();

        struct EvalResultLine
        {
            public int block; // 0 活 1 半活 2 死
            public int count; // 联珠数
        }
        struct EvalResult
        {
            public int[] block;
            public int count; // 重复数
        }

        Random random = new Random();

        Image img;
        Graphics g;

        public frmFive()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DrawBoard(pic);
        }

        private void DrawBoard(PictureBox pic, bool reset = true)
        {
            if (reset == true)
            {
                board = new int[intSize, intSize];
                intStatus = 1; // (chkBlackFirst.Checked == true) ? 1 : 2;
            }
            CheckLblStatus();

            img = new Bitmap(intBlock * (intSize + 1) + 1, intBlock * (intSize + 1) + 1);
            g = Graphics.FromImage(img);

            g.Clear(Color.White);
            for (int i = 0; i <= (intSize + 1); i++)
            {
                g.DrawLine(Pens.Black, 0, intBlock * i, intBlock * (intSize + 1), intBlock * i);
                g.DrawLine(Pens.Black, intBlock * i, 0, intBlock * i, intBlock * (intSize + 1));
            }

            if (reset == false)
            {
                Brush brush = null;
                Pen pen = new Pen(Color.Black);
                for (int i = 0; i < intSize; i++)
                {
                    for (int j = 0; j < intSize; j++)
                    {
                        if (board[i, j] != 0)
                        {
                            brush = new SolidBrush((board[i, j] == 1) ? Color.Black : Color.White);
                            g.FillEllipse(brush, (int)(intBlock * (i + 0.5)), (int)(intBlock * (j + 0.5)), intBlock, intBlock);
                            g.DrawEllipse(pen, (int)(intBlock * (i + 0.5)), (int)(intBlock * (j + 0.5)), intBlock, intBlock);
                        }
                    }
                }
            }

            pic.Image = img;

            if (reset == true)
            {
                if (chkBlackFirst.Checked == false) PlayBoard(intSize / 2, intSize / 2);
                //chkAIenable.Enabled = false;
                //chkBlackFirst.Enabled = false;
            }

        }
        
        private void CheckLblStatus()
        {
            switch (intStatus)
            {
                case 0:
                    lblStatus.Text = "游戏尚未开始"; break;
                case 1:
                    lblStatus.Text = "轮 执黑棋者 下一手"; break;
                case 2:
                    lblStatus.Text = "轮 执白棋者 下一手"; break;
                case 3:
                    lblStatus.Text = "执黑棋者 获胜"; break;
                case 4:
                    lblStatus.Text = "执白棋者 获胜"; break;
            }
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            int bX = (e.X + intBlock / 2) / intBlock;
            int bY = (e.Y + intBlock / 2) / intBlock;
            if (PlayBoard(bX - 1, bY - 1) != 0 && chkAIenable.Checked == true && intStatus < 3) AIplay();
        }

        // 落子，成功返回 1
        private int PlayBoard(int X, int Y)
        {
            if (X < 0 || X >= intSize || Y < 0 || Y >= intSize) return 0;
            Brush brush = null;
            Pen pen = new Pen(Color.Black);
            switch (intStatus)
            {
                case 0:
                    MessageBox.Show("请先开始新游戏");
                    return 0;
                case 1:
                    brush = new SolidBrush(Color.Black); break;
                case 2:
                    brush = new SolidBrush(Color.White); break;
                case 3:
                    MessageBox.Show("黑方已胜，请先开始新游戏");
                    return 0;
                case 4:
                    MessageBox.Show("白方已胜，请先开始新游戏");
                    return 0;
            }
            if (board[X, Y] == 0)
            {
                board[X, Y] = intStatus;
                g.FillEllipse(brush, (int)(intBlock * (X + 0.5)), (int)(intBlock * (Y + 0.5)), intBlock, intBlock);
                g.DrawEllipse(pen,   (int)(intBlock * (X + 0.5)), (int)(intBlock * (Y + 0.5)), intBlock, intBlock);
                pic.Image = img;

                regret.Push(new PointEx() { x = X, y = Y, state = intStatus });

                intStatus += (EvalLine(board, X, Y)[5].count != 0) ? 2 : 0; // 判断胜方

                if (intStatus < 3)
                {
                    intStatus ^= 3; // 没赢轮换
                }
                //else
                //{
                //    chkAIenable.Enabled = true;
                //    chkBlackFirst.Enabled = true;
                //}
                CheckLblStatus();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void AIplay()
        {
            // 计算可落子点的权重
            int[,] score = new int[intSize, intSize];
            int attack, defend;
            for (int i = 0; i < intSize; i++)
            {
                for (int j = 0; j < intSize; j++)
                {
                    if (board[i, j] != 0)
                    {
                        score[i, j] = -1;
                    }
                    else
                    {
                        // if (i == 9 && j == 3) Debug.WriteLine("");
                        attack = EvalChess(i, j, intStatus);
                        defend = EvalChess(i, j, intStatus ^ 3);
                        score[i, j] = (attack >= defend) ? attack : defend;
                    }
                }
            }

            Debug.Write("\n落子权重表\n");
            for (int j = 0; j < intSize; j++)
            {
                //Debug.Write(j);
                //Debug.Write("\t|\t");
                for (int i = 0; i < intSize; i++)
                {
                    Debug.Write(score[i, j]);
                    Debug.Write('\t');
                }
                Debug.Write('\n');
            }

            // 取最高分点落子
            int maxScore = 0;
            List<Point> pieces = new List<Point>();
            for (int i = 0; i < intSize; i++)
            {
                for (int j = 0; j < intSize; j++)
                {
                    if (score[i, j] > maxScore)
                    {
                        maxScore = score[i, j];
                        pieces.Clear();
                        pieces.Add(new Point(i, j));
                    }
                    else if (score[i, j] == maxScore)
                    {
                        pieces.Add(new Point(i, j));
                    }
                }
            }
            if (pieces.Count != 0)
            {
                int index = random.Next(0, pieces.Count - 1);
                PlayBoard(pieces[index].X, pieces[index].Y);
            }
            else // 无路可走，人获胜
            {
                intStatus = 3;
                //chkAIenable.Enabled = true;
                //chkBlackFirst.Enabled = true;
                CheckLblStatus();
            }
        }

        // 评估落子后棋盘得分
        private int EvalChess(int x, int y, int side)
        {
            int[,] boardTmp = new int[intSize, intSize];
            Array.Copy(board, boardTmp, board.Length);
            boardTmp[x, y] = side;

            EvalResult[] ret = EvalLine(boardTmp, x, y);
            //Debug.Write(count + "\t");

            int score = 0;

            int i, dbl, rev;

            if (ret[5].count != 0)
            {
                // 100 能成5
                score = 100;
            }
            else if (ret[4].count != 0)
            {
                // 90 能成活4、双死4、死4活3
                for (i = 0, dbl = 0; i < ret[4].count; i++)
                {
                    if (ret[4].block[i] >= 2) // 活4
                    {
                        score = 90; break;
                    }
                    else
                    {
                        dbl++;
                        if (dbl == 2) // 双死4
                        {
                            score = 90; break;
                        }
                    }
                }
                if (score == 0 && ret[3].count != 0)
                    for (i = 0; i < ret[3].count; i++)
                        if (ret[3].block[i] >= 2) // 死4活3
                        {
                            score = 90; break;
                        }
                if (score == 0) score = 5; // 死4
            }
            else if (ret[3].count != 0)
            {
                // 10 死3
                // 20 死3活2
                // 40 活3
                // 45 死3活3
                // 80 双活3
                for (i = 0, dbl = 0, rev = 0; i < ret[3].count; i++)
                {
                    if (ret[3].block[i] >= 2) // （双）活3
                    {
                        rev |= 2;
                        dbl++;
                        if (dbl == 2)
                        {
                            score = 80; break;
                        }
                    }
                    else 
                        rev |= 1;
                }
                if (score == 0)
                {
                    if (rev == 3)
                        score = 45; // 死3活3
                    else if (rev == 2)
                        score = 40; // 活3
                    else
                    {
                        for (i = 0; i < ret[2].count; i++)
                            if (ret[2].block[i] >= 2)
                            {
                                score = 20; // 死3活2
                                break;
                            }

                    }
                    if (score == 0) score = 10; // 死3
                }
            }
            else if (ret[2].count != 0)
            {
                // 1 能成死2
                // 5 能成活2
                // 15 能成双活2
                for (i = 0, dbl = 0; i < ret[2].count; i++)
                {
                    if (ret[2].block[i] >= 2) // （双）活2
                    {
                        score += 5;
                        dbl++;
                        if (dbl == 2) break;
                    }
                }
                if (score == 0) score = 1; // 死2
            }

            return score + ((intStatus == side) ? 1 : 0); // 进攻优先
        }

        // 计算联珠数
        private EvalResult[] EvalLine(int[,] board, int x, int y)
        {
            int i, j, flag;
            EvalResultLine line;
            EvalResult[] ret = new EvalResult[6]; // 使用 1~5
            for (i = 0; i < 6; i++)
                ret[i].block = new int[4];

            // 【——】
            i = x;
            flag = 0; line = new EvalResultLine();
            for (j = 0; j < intSize; j++)
            {
                if (EvalLine_p1(board, x, y, i, j, ref flag, ref line) != 0) break;
            }
            ret[line.count].block[ret[line.count].count] = line.block;
            ret[line.count].count++;

            // 【｜】
            j = y;
            flag = 0; line = new EvalResultLine();
            for (i = 0; i < intSize; i++)
            {
                if (EvalLine_p1(board, x, y, i, j, ref flag, ref line) != 0) break;
            }
            ret[line.count].block[ret[line.count].count] = line.block;
            ret[line.count].count++;

            // 【＼】
            flag = 0; line = new EvalResultLine();
            if (x >= y) // 右上部，从上边起
            {
                i = x - y;
                for (j = 0; i + j != intSize; j++)
                {
                    if (EvalLine_p1(board, x, y, i + j, j, ref flag, ref line) != 0) break;
                }
            }
            else // 左下部，从左边起
            {
                j = y - x;
                for (i = 0; i + j != intSize; i++)
                {
                    if (EvalLine_p1(board, x, y, i, i + j, ref flag, ref line) != 0) break;
                }
            }
            ret[line.count].block[ret[line.count].count] = line.block;
            ret[line.count].count++;

            // 【／】
            flag = 0; line = new EvalResultLine();
            if (x + y < intSize) // 左上部，从上边起
            {
                i = x + y;
                for (j = 0; i - j >= 0; j++)
                {
                    if (EvalLine_p1(board, x, y, i - j, j, ref flag, ref line) != 0) break;
                }
            }
            else // 右下部，从右边起
            {
                j = x + y - intSize + 1;
                for (i = 0; i + j != intSize; i++)
                {
                    if (EvalLine_p1(board, x, y, intSize - i - 1, i + j, ref flag, ref line) != 0) break;
                }
            }
            ret[line.count].block[ret[line.count].count] = line.block;
            ret[line.count].count++;

            return ret;
        }

        private int EvalLine_p1(int[,] board, int x, int y, int i, int j, ref int flag, ref EvalResultLine ret)
        {
            if (board[i, j] == board[x, y])
            {
                ret.block = (ret.block != 0) ? 1 : 0;
                ret.count++;
                if (i == x && j == y)
                {
                    flag = 1;
                }
            }
            else if (flag == 0) // 不包括当前子，要重置
            {
                if (board[i, j] == 0) ret.block++;
                ret.count = 0;
            }
            else // 包括当前子的联珠已算完，停下来
            {
                if (board[i, j] == 0) ret.block++;
                ret.count = (ret.count >= 5) ? 5 : ret.count;
                return 1;
            }
            return 0; // 不包括，继续
        }

        private void frmFive_Load(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DrawBoard(pic);

            //chkAIenable.Enabled = true;
            //chkBlackFirst.Enabled = true;
            intStatus = 0;
            CheckLblStatus();
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        private void btnRegret_Click(object sender, EventArgs e)
        {
            if (regret.Count == 0) return;
            PointEx p = regret.Pop();
            board[p.x, p.y] = 0;
            intStatus = p.state;
            DrawBoard(pic, false);
        }

    }
}
