using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllTest
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public string step
        {
            get
            { return Status.myTransmit(x) + Status.myTransmit(y); }
        }
    }
    /// <summary>
    /// 表示一个棋盘局面
    /// </summary>
    public class Status
    {
        public sbyte[,] s;
        public Status()
        {
            s = new sbyte[19, 19];
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    s[i,j] = 0;
        }
        public Status clone()
        {
            Status next = new Status();
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    next.s[i, j] = this.s[i, j];

            return next;
        }
        /// <summary>
        /// 在控制台以矩阵方式显示当前局面
        /// </summary>
        /// <returns></returns>
        public string dis()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (s[i, j] != -1)
                        sb.Append(s[i, j] + " ");
                    else sb.Append("X ");
                }
                sb.Append("\n");
            }
            return sb.ToString() + this.valuate().ToString();
        }
        /// <summary>
        /// 下一个子后生成的新局面
        /// </summary>
        /// <param name="x">新子的x坐标</param>
        /// <param name="y">新子的y坐标</param>
        /// <param name="IsMe">是自己落子还是对方落子</param>
        /// <returns></returns>
        public Status next(Int16 x, Int16 y,bool IsMe)
        {
            Status next = new Status();
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    next.s[i, j] = this.s[i, j];

            next.s[x, y] = IsMe ? (sbyte)1 : (sbyte)(-1);
            return next;
        }
        /// <summary>
        /// 下两个子后生成的新局面
        /// </summary>
        /// <param name="x1">第一个新子的x坐标</param>
        /// <param name="y1">第一个新子的y坐标</param>
        /// <param name="x2">第二个新子的x坐标</param>
        /// <param name="y2">第二个新子的y坐标</param>
        /// <param name="IsMe">是自己落子还是对方落子</param>
        /// <returns></returns>
        public Status realNext(Int16 x1, Int16 y1, Int16 x2, Int16 y2, bool IsMe)
        {
            Status next = new Status();
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    next.s[i, j] = this.s[i, j];

            next.s[x1, y1] = IsMe ? (sbyte)1 : (sbyte)(-1);
            next.s[x2, y2] = IsMe ? (sbyte)1 : (sbyte)(-1);
            return next;
        }
        /// <summary>
        /// 下一个子后可能生成的所有新局面
        /// </summary>
        /// <param name="IsMe">是自己落子还是对方落子</param>
        /// <returns></returns>
        public List<Status> nextStep(bool IsMe)
        {
            List<Status> res = new List<Status>();

             for (Int16 i = 1; i < 18; i++)
                 for (Int16 j = 1; j < 18; j++)
                 {
                     if (s[i, j] == 0 )
                     {
                         if (i > 1 && i < 17 && j > 1 && j < 17&&
                             (s[i - 1, j - 1] != 0 | s[i - 1, j] != 0 | s[i - 1, j + 1] != 0
                             | s[i, j - 1] != 0 | s[i, j + 1] != 0 | s[i + 1, j - 1] != 0 | s[i + 1, j] != 0 | s[i + 1, j + 1] != 0))
                            // | s[i - 2, j - 2] != 0 | s[i - 2, j - 1] != 0 | s[i - 2, j] != 0 | s[i - 2, j + 1] != 0 | s[i - 2, j+2] != 0
                            //  | s[i - 1, j - 2] != 0 | s[i - 1, j + 2] != 0 | s[i, j - 2] != 0 | s[i, j + 2] != 0 | s[i +1, j-2] != 0
                            //   | s[i + 1, j + 2] != 0 | s[i + 2, j + 2] != 0 | s[i + 2, j + 1] != 0 | s[i + 2, j ] != 0
                           //  | s[i + 2, j - 1] != 0 | s[i + 2, j - 2] != 0)   )
                         {
                             res.Add(this.next(i, j, IsMe));
                         }
                         else if((i==1||i==17)&&(j==1||j==17)&&
                             (s[i - 1, j - 1] != 0 | s[i - 1, j] != 0 | s[i - 1, j + 1] != 0 | s[i, j - 1] != 0 | s[i, j + 1] != 0 
                             | s[i + 1, j - 1] != 0 | s[i + 1, j] != 0 | s[i + 1, j + 1] != 0))
                             res.Add(this.next(i, j, IsMe));
                     }
                 }
             return res;
        }
      /// <summary>
        /// 下一个子后可能生成的所有新局面
      /// </summary>
      /// <param name="IsMe">无意义</param>
      /// <returns>所以新落子点的集合</returns>
        public Dictionary<string, Point> nextStep3(bool IsMe)
        {
            Dictionary<string, Point> res = new Dictionary<string, Point>();
            for (Int16 i = 1; i < 18; i++)
                for (Int16 j = 1; j < 18; j++)
                    if (s[i, j] == 0 && (s[i - 1, j - 1] != 0 | s[i - 1, j] != 0 | s[i - 1, j + 1] != 0 | s[i, j - 1] != 0 | s[i, j + 1] != 0 | s[i + 1, j - 1] != 0 | s[i + 1, j] != 0 | s[i + 1, j + 1] != 0))
                    {
                        res.Add(myTransmit(i) + myTransmit(j), new Point { x=i,y=j});
                    }
            return res;
        }
        //public List<Status> RealNext(bool IsMe)
        //{
        //    List<Status> res = new List<Status>();
        //    foreach (Status st in this.nextStep(IsMe))
        //    {
        //        res.AddRange(st.nextStep(IsMe));
        //    }
        //    return res;
        //}
        /// <summary>
        /// 为不同的旗形计算分值
        /// </summary>
        /// <param name="sum">六个连续格中的同种棋子数,己方为正,对方为负</param>
        /// <returns>分值</returns>
        public int valueRank(int sum)
        {
           
                switch (sum)
                {
                    case 3: return  2;
                    case 4: return  10;
                    case 5: return  80;
                    case 6: return  9000;
                    case -3: return -2;
                    case -4: return -10;
                    case -5: return -80;
                    case -6: return -9000;
                    default: return 0;
                }
        }
       
        //public HashSet<Status> nextWithValuate(bool IsMe)
        //{
        //    HashSet<Status> res=new HashSet<Status>();
        //    int black = 0, white = 0;
        //    Stack<Point> space=new Stack<Point>();
            
        //  //  int tem; int flag = 0; bool fff = true;
        //    //横向估值己方
        //    for (int i = 0; i < 19; i++)
        //        for (int j = 0; j < 14; j++)
        //        {
        //            for (int q = 0; q <= 5; q++)
        //                switch (s[i, j + q])
        //                {
        //                    case 1: black++; break;
        //                    case -1: white++; break;
        //                    default: space.Push(new Point{x=i,y=j+q}); break;
        //                }
        //            if( (white>=4 && black==0) || (white==0&&black>=2) )
        //                while(space.Count!=0)
        //                {
        //                    Point po=space.Pop();
        //                    res.Add(this.next((Int16)po.x, (Int16)po.y, IsMe));//TODO:还应判断邻个有子时才能放。
        //                }
        //        }
        //    //for (int i = 0; i < 19; i++)
        //    //    for (int j = 0; j < 14; j++)
        //    //    {
        //    //        for (int q = 5; q >= 0; q--)//判断这六个格中是否有白子
        //    //        {
        //    //            if (s[i, j + q] == -1)
        //    //            {
        //    //                flag = q;
        //    //                fff = false;
        //    //                break;
        //    //            }
        //    //        }
        //    //        if (!fff) { j += flag; flag = 0; fff = true; continue; }//若有白子则跳过
        //    //        tem = s[i, j] + s[i, j + 1] + s[i, j + 2] + s[i, j + 3] + s[i, j + 4] + s[i, j + 5];//计算黑子的数量
        //    //        if (tem >= 2)
        //    //        {
        //    //            for (int m = j; m <= j + 5; m++)
        //    //            {
        //    //                if (s[i, m] == 0)
        //    //                {
        //    //                    if(
        //    //                        (m==0 && s[i,m+1]!=0)
        //    //                        ||(m==j+5 && s[i,m-1]!=0) 
        //    //                        || (m!=0 && m!=j+5 && (s[i, m - 1] != 0 || s[i, m + 1] != 0))
        //    //                        )
        //    //                    res.Add(this.next((Int16)i, (Int16)m, true));
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //}
        public List<Point> meaningfulNext()
        {
            List<Point> res = new List<Point>();
            for(int i=0;i<=18;i++)
                for (int j = 0; j <= 18; j++)
                {
                    if (s[i, j] == 0)//((i-2<0 || j-2<0)||(i-2>=0&&j-2>=0&&s[i-2,j-2]!=0))
                    {
                        if (i - 2 >= 0 && j - 2 >= 0 && s[i - 2, j - 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 2 >= 0 && j - 1 >= 0 && s[i - 2, j - 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 2 >= 0 && s[i - 2, j ] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 2 >= 0 && j + 2 <= 18 && s[i - 2, j + 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 2 >= 0 && j +1 <=18 && s[i - 2, j +1] != 0) { res.Add(new Point { x = i, y = j }); continue; }

                        if (i - 1 >= 0 && j - 2 >= 0 && s[i - 1, j - 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 1 >= 0 && j - 1 >= 0 && s[i - 1, j - 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 1 >= 0 && s[i - 1, j] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 1 >= 0 && j + 2 <= 18 && s[i - 1, j + 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i - 1 >= 0 && j + 1 <= 18 && s[i - 1, j +1] != 0) { res.Add(new Point { x = i, y = j }); continue; }

                        if ( j - 2 >= 0 && s[i , j - 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if ( j - 1 >= 0 && s[i, j - 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if ( j + 2 <=18 && s[i , j + 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if ( j +1<=18 && s[i , j +1] != 0) { res.Add(new Point { x = i, y = j }); continue; }

                        if (i + 1 <=18 && j - 2 >= 0 && s[i + 1, j - 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 1 <= 18 && j - 1 >= 0 && s[i + 1, j - 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 1 <= 18 && s[i+ 1, j] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 1 <= 18 && j + 2 <= 18 && s[i+ 1, j + 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 1 <= 18 && j + 1 <= 18 && s[i + 1, j + 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }

                        if (i + 2 <=18 && j - 2 >= 0 && s[i + 2, j - 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 2 <= 18 && j - 1 >= 0 && s[i +2, j - 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 2 <= 18 && s[i + 2, j] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 2 <= 18 && j + 2 <= 18 && s[i + 2, j + 2] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                        if (i + 2 <= 18 && j + 1 <= 18 && s[i + 2, j + 1] != 0) { res.Add(new Point { x = i, y = j }); continue; }
                    }
                }
            return res;
        }
        /// <summary>
        /// 对该局面评分
        /// </summary>
        /// <returns></returns>
        public Int16 valuate()
        {
            int value = 0, tem; int flag=0 ; bool fff=true;
            //横向估值己方
             for (int i = 0; i < 19; i++)
                 for (int j = 0; j < 14; j++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有白子
                     {
                         if (s[i,j+q] == -1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { j += flag; flag = 0; fff = true; continue; }//若有白子则跳过
                     tem = s[i, j] + s[i, j + 1] + s[i, j + 2] + s[i, j + 3] + s[i, j + 4] + s[i, j + 5];//计算黑子的数量
                     value += valueRank(tem);
                   
                 }
             //横向估值对方
             for (int i = 0; i < 19; i++)
                 for (int j = 0; j < 14; j++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有黑子
                     {
                         if (s[i, j + q] == 1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { j += flag; flag = 0; fff = true; continue; }//若有黑子则跳过
                     tem = s[i, j] + s[i, j + 1] + s[i, j + 2] + s[i, j + 3] + s[i, j + 4] + s[i, j + 5];//计算白子的数量
                     value += valueRank(tem);
                 }
             //纵向估值己方
             for (int i = 0; i < 19; i++)
                 for (int j = 0; j < 14; j++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有白子
                     {
                         if (s[j+q,i] == -1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { j += flag; flag = 0; fff = true; continue; }//若有白子则跳过
                     tem = s[j,i] + s[ j + 1,i] + s[ j + 2,i] + s[j + 3,i] + s[ j + 4,i] + s[j + 5,i];//计算黑子的数量
                     value += valueRank(tem);
                 }
           //纵向估值对方
             for (int i = 0; i < 19; i++)
                 for (int j = 0; j < 14; j++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有黑子
                     {
                         if (s[j + q, i] == 1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { j += flag; flag = 0; fff = true; continue; }//若有黑子则跳过
                     tem = s[j,i] + s[j + 1, i] + s[j + 2, i] + s[j + 3, i] + s[j + 4, i] + s[j + 5, i];//计算白子的数量
                     value += valueRank(tem);
                 }
            //斜向(右下)估值己方
             for (int i = 12, j = 0; i >= 0 && j <= 12; i-=(i==0?0:1), j+=(i==0?1:0))
                 for (int k = 0; i + k < 19-5 && j + k < 19-5; k++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有白子
                     {
                         if (s[i+k+q,j+k+q] == -1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { k += flag; flag = 0; fff = true; continue; }//若有白子则跳过
                     tem = s[i + k, j + k] + s[i + k + 1, j + k + 1] + s[i + k + 2, j + k + 2]
                         + s[i + k + 3, j + k + 3] + s[i + k + 4, j + k + 4] + s[i + k + 5, j + k + 5];//计算黑子的数量
                     value += valueRank(tem);
                 }
             //斜向(右下)估值对方
             for (int i = 12, j = 0; i >= 0 && j <= 12; i -= (i == 0 ? 0 : 1), j += (i == 0 ? 1 : 0))
                 for (int k = 0; i + k < 19-5 && j + k < 19-5; k++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有黑子
                     {
                         if (s[i + k + q, j + k + q] == 1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { k += flag; flag = 0; fff = true; continue; }//若有黑子则跳过
                     tem = s[i + k, j + k] + s[i + k + 1, j + k + 1] + s[i + k + 2, j + k + 2]
                         + s[i + k + 3, j + k + 3] + s[i + k + 4, j + k + 4] + s[i + k + 5, j + k + 5];//计算白子的数量
                     value += valueRank(tem);
                 }
             //斜向(右上)估值对方
             for (int i = 5, j = 0; i < 19 && j <= 12; i += (i == 18 ? 0 : 1), j += (i == 18 ? 1 : 0))
                 for (int k = 0; i - k >=5 && j + k < 19-5; k++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有黑子
                     {
                         if (s[i - k - q, j + k + q] == 1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { k += flag; flag = 0; fff = true; continue; }//若有黑子则跳过
                     tem = s[i - k, j + k] + s[i - k - 1, j + k + 1] + s[i - k - 2, j + k + 2]
                         + s[i - k - 3, j + k + 3] + s[i - k - 4, j + k + 4] + s[i - k - 5, j + k + 5];//计算白子的数量
                     value += valueRank(tem);
                 }
             //斜向(右上)估值己方
             for (int i = 5, j = 0; i < 19 && j <= 12; i += (i == 18 ? 0 : 1), j += (i == 18 ? 1 : 0))
                 for (int k = 0; i - k >= 5 && j + k < 19 - 5; k++)
                 {
                     for (int q = 5; q >= 0; q--)//判断这六个格中是否有白子
                     {
                         if (s[i - k - q, j + k + q] == -1)
                         {
                             flag = q;
                             fff = false;
                             break;
                         }
                     }
                     if (!fff) { k += flag; flag = 0; fff = true; continue; }//若有白子则跳过
                     tem = s[i - k, j + k] + s[i - k - 1, j + k + 1] + s[i - k - 2, j + k + 2]
                         + s[i - k - 3, j + k + 3] + s[i - k - 4, j + k + 4] + s[i - k - 5, j + k + 5];//计算黑子的数量
                     value += valueRank(tem);
                 }
             return (value == 0) ? (Int16)1 : (Int16)value;
        }
    
        private string Transmit(int i)
        {
            switch (i)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
                case 8: return "I";
                case 9: return "J";
                case 10: return "K";
                case 11: return "L";
                case 12: return "M";
                case 13: return "N";
                case 14: return "O";
                case 15: return "P";
                case 16: return "Q";
                case 17: return "R";
                case 18: return "S";
                default: return "";
            }
        }
        public static  string myTransmit(int i)
        {
            switch (i)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
                case 8: return "I";
                case 9: return "J";
                case 10: return "K";
                case 11: return "L";
                case 12: return "M";
                case 13: return "N";
                case 14: return "O";
                case 15: return "P";
                case 16: return "Q";
                case 17: return "R";
                case 18: return "S";
                default: return "";
            }
        }
        private sbyte Transmit(char c)
        {
            switch (c)
            {
                case 'A': return 0;
                case 'B': return 1;
                case 'C': return 2;
                case 'D': return 3;
                case 'E': return 4;
                case 'F': return 5;
                case 'G': return 6;
                case 'H': return 7;
                case 'I': return 8;
                case 'J': return 9;
                case 'K': return 10;
                case 'L': return 11;
                case 'M': return 12;
                case 'N': return 13;
                case 'O': return 14;
                case 'P': return 15;
                case 'Q': return 16;
                case 'R': return 17;
                case 'S': return 18;
                default: return -1;
            }
        }
        /// <summary>
        /// 根据走法更新棋局,是对本Status的更改而next()是生成新的Status
        /// </summary>
        /// <param name="mov">走步</param>
        /// <param name="IsMe">己方还是对方</param>
        public void AddPiece(string mov,bool IsMe)
        {
            this.s[Transmit(mov[0]), Transmit(mov[1])] = IsMe ? (sbyte)1 :(sbyte)( -1);
        }
        public void AddPiece(Point p, bool IsMe)
        {
            this.s[p.x,p.y]=IsMe ? (sbyte)1 :(sbyte)( -1);
        }
        /// <summary>
        /// 比较两个棋局的不同,用于表示走步
        /// </summary>
        /// <param name="p">另一棋局</param>
        /// <returns>导致这一不同的走步方式</returns>
        public string compare(Status p)
        {
            StringBuilder r = new StringBuilder();
            Int16 flag = 2;

            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    if (this.s[i, j] != p.s[i, j])
                    {
                        r.Append(Transmit(i)).Append(Transmit(j));
                        flag--;
                        if (flag == 0) return r.ToString();
                    }
            
            return "";
        }
    }
    public class Node
    {
        public Node parent;
        public List<Node> subNodes;
        public Status data;
        public Int16 value;
        public string step;
        public Node(Node p, Status d,string s)
        {
            parent = p;
            data = d;
            value = 0;
            step = s;
            subNodes = new List<Node>();
        }
        
        public void AddSub(Node sub)
        {
            StringBuilder sb = new StringBuilder(sub.step.Substring(2));
            sb.Append(sub.step.Substring(0, 2));
            if(!this.subNodes.Exists(p=>p.step==sb.ToString()) && !this.subNodes.Exists(p=>p.step==sub.step))
           // if (this.step != sb.ToString() && this.step != sub.step)
                this.subNodes.Add(sub);
        }
        /// <summary>
        /// 通过与父节点的比较找出导致该节点的走步
        /// </summary>
        /// <returns>导致该节点的走步</returns>
        public string MakeStep()
        {
            if (this.parent == null)
                return "";
            else return this.step;//(this.data.compare(parent.data));
        }
    }
    public class Move
    {
        public Node root;//当前局面
        public Move(Status st)
        {
            root = new Node(null, st,"");
        }
        /// <summary>
        /// 创建博弈树
        /// </summary>
        /// <param name="node">根节点</param>
        /// <param name="layel">层数</param>
        public void  CreateTree(Node node,  Int16 layel)
        {
            if (layel == 0)
            {
                node.value = node.data.valuate();
                //alpha-beta
                Int16 max = node.parent.parent.subNodes.Max(e => e.value);
                if (max != 0 && node.value < max)
                    node.parent.value = node.value;
            }
            else
            {
                Point[] arr = node.data.meaningfulNext().ToArray();//TODO:每次落子后将落子点从列表中删除，再根据落子添加新点，无需每次重新扫描。5、11
                for (int i = 0; i < arr.Length; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        Status st = node.data.clone();
                        st.AddPiece(arr[i], layel % 2 == 0);
                        st.AddPiece(arr[j], layel % 2 == 0);
                        Node nst = new Node(node, st, arr[i].step+arr[j].step);
                        node.AddSub(nst);
                        CreateTree(nst, (Int16)(layel - 1));
                        if (node.value != 0)
                        {
                            node.subNodes.Clear();
                            break;
                        }
                    }
                    if (node.value != 0)
                    {
                        node.subNodes.Clear();
                        break;
                    }
                }
                /*foreach (var st in node.data.nextStep3(layel % 2 == 0))//node.data.nextStep(layel % 2 == 0))
                {
                    for (Int16 i = 1; i < 18; i++)
                    {
                        for (Int16 j = 1; j < 18; j++)
                        {
                            sbyte memo = node.data.s[(Int16)st.Value.x, (Int16)st.Value.y];
                            node.data.s[(Int16)st.Value.x, (Int16)st.Value.y] = layel % 2 == 0 ? (SByte)1 : (SByte)(-1);
                            Status tem = node.data; //node.data.next((Int16)st.Value.x, (Int16)st.Value.y, layel % 2 == 0);
                            if (tem.s[i, j] == 0 && (tem.s[i - 1, j - 1] != 0 | tem.s[i - 1, j] != 0 | tem.s[i - 1, j + 1] != 0 |
                                tem.s[i, j - 1] != 0 | tem.s[i, j + 1] != 0 | tem.s[i + 1, j - 1] != 0 | tem.s[i + 1, j] != 0 |
                                tem.s[i + 1, j + 1] != 0))
                            {
                                Node nst = new Node(node, tem.next( i, j, layel % 2 == 0), st.Key+Status.myTransmit(i) + Status.myTransmit(j));
                                node.AddSub(nst);
                                CreateTree(nst, (Int16)(layel - 1));
                                //alpha-beta  3.30很可能不对。
                                if (node.value != 0)
                                {
                                    node.subNodes.Clear();
                                    break;
                                }
                            }
                            node.data.s[(Int16)st.Value.x, (Int16)st.Value.y] = memo;
                        }
                        if (node.value != 0)
                        {
                            node.subNodes.Clear();
                            break;
                        }
                    }
                    if (node.value != 0)
                    {
                        node.subNodes.Clear();
                        break;
                    }
                }*/
                if (node.value == 0)
                {
                    //预测两步
                    var tem = node.subNodes.OrderBy(e => e.value);
                    Node temnode;
                    if (layel % 2 == 0) { node.value = tem.Last().value; temnode = tem.Last(); }
                    else{  node.value =tem.First().value; temnode=tem.First();}//node.subNodes.Min(e => e.value);

                    node.subNodes.Clear();
                    node.subNodes.Add(temnode);
                    //if (node != this.root) node.subNodes.Clear();
                }

            }
        }
       
    }
    public class Class1
    {
        static  Status st;
        static  Move move;
        public static string response(string msg)
        {
            switch (msg.Substring(0, 3))
            {
                case "dis":
                    {
                        for (int k = 16, l = 4; k > 10 && l < 10; k--, l++)
                            st.s[k, l] = -1;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < 19; i++)
                        {
                            for (int j = 0; j < 19; j++)
                            {
                                if (st.s[i, j] != -1)
                                    sb.Append(st.s[i, j] + " ");
                                else sb.Append("X ");
                            }
                            sb.Append("\n");
                        }
                        return sb.ToString()+st.valuate().ToString();
                    }

                case "new":
                    {
                        st = new Status();
                        if (msg.EndsWith("black"))
                        {
                            st.s[10, 10] = 1;
                            return "move KK@@";
                        }
                        else return "name ouc";
                    }
                case "mov":
                    {
                        string countStep = msg.Substring(5);
                        if (countStep.Contains("@"))
                            st.AddPiece(countStep.Substring(0,2), false);
                        else
                        {
                            st.AddPiece(countStep.Substring(0,2),false);
                            st.AddPiece(countStep.Substring(2), false);
                        }

                        move = new Move(st);
                        move.CreateTree(move.root, 2);
                        Node rn = move.root.subNodes.First();//(e => e.value == move.root.value);
                        string myStep=rn.MakeStep();
                        st.AddPiece(myStep.Substring(0, 2), true);
                        st.AddPiece(myStep.Substring(2), true);
                        return "move " + myStep+"\n" + rn.data.dis() + "\n" + rn.subNodes.First().data.dis();
                        
                    }
                case "end": return "name oucAI";
                case "nam": return "name oucAI";
                default: return "name oucAI";
            }
        }
    }
}
