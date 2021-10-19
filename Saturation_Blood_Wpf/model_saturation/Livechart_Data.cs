using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Helpers;
using System.Windows;
using System.Windows.Media;
using LiveCharts.Defaults;

namespace Saturation_Blood_Wpf.model_saturation
{
    public class Livechart_Data
    {
        private int size_min;

        public void Set_saze_min(int value) {
            size_min = value;
        }

        const int potok2 = 13; //Общее Число потоков (работае только 4 + время)
        long shift_grafh = 200;
        long shift_grafh_ekg = 200;
        long maximum = 0;
        long minimum = 1024;              

        Initial_processing.Initial_Data initdata;
        Points.Special_point special_Point;

        Points.Special_point special_Point_red_const;
        Points.Special_point special_Point_red_diff;
        Points.Special_point special_Point_IK_const;
        Points.Special_point special_Point_IK_diff;

        private const double LineSmouth = 0.0;

        private string[] _labels;

        public double Step_Y;
        private SeriesCollection SeriesCollection;

        private string Axis_Title_Left = "Амплитуда";
        private string Axis_Title_Right = "";
        private bool Is_Show_Label_ = false;

        public SeriesCollection Get_SeriesCollection() 
        {
            return SeriesCollection;
        }

        public string Get_Axis_Title_Right() 
        {
            return Axis_Title_Right;
        }

        public string Get_Axis_Title_Left()
        {
            return Axis_Title_Left;
        }

        public bool Get_Is_Show_Label_() {

            return Is_Show_Label_;
        }

        public string[] Get_Labels() {

            return _labels;
        }

        private SolidColorBrush _abColor_1 = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _abColor_2 = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _abColor_3 = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _abColor_4 = new SolidColorBrush(Colors.Blue);

        public Livechart_Data(Initial_processing.Initial_Data init)
        {
            initdata = init;           
        }

        public Livechart_Data(Initial_processing.Initial_Data init, Points.Special_point _Point)
        {
            initdata = init;
            special_Point = _Point;
        }

        public Livechart_Data(Initial_processing.Initial_Data init, Points.Special_point _Point_r_c,
        Points.Special_point _Point_r_d, Points.Special_point _Point_i_c, Points.Special_point _Point_i_d)
        {
            initdata = init;

            special_Point_red_const = _Point_r_c;
            special_Point_red_diff = _Point_r_d;
            special_Point_IK_const = _Point_i_c;
            special_Point_IK_diff = _Point_i_d;
        }

        /// <summary>
        /// Построить график с 4 каналов
        /// </summary>
        public void MakeGraph_4_Canal_Observable()
        {
            long[,] data = Converter_Data_for_Livecharts.Minimize_Row_1(initdata.Get_row1(), size_min, initdata.Get_b());
            int size_data = initdata.Get_b();

            _labels = Converter_Data_for_Livecharts.Convert_data_X_for_Labels(data);
            int labels_data_length = _labels.GetUpperBound(0) + 1;

            ChartValues<ObservablePoint> val_1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_4 = new ChartValues<ObservablePoint>();


            for (int i = 0; i < labels_data_length; i++)
            {
                val_1.Add(new ObservablePoint
                {
                X = Convert.ToDouble(data[i, 0])/1000000,
                Y = data[i, 1]
                });

                val_2.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 2]
                });
                val_3.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 3]
                });
                val_4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 4]
                });

            }
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Канал 1",
                    Values = val_1,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent

                },

               new LineSeries
                {
                    Title = "Канал 2",
                    Values = val_2,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent

                },
                 new LineSeries
                {
                    Title = "Канал 3",
                    Values = val_3,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent

                },
                  new LineSeries
                {
                    Title = "Канал 4",
                    Values = val_4,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent

                }

            };
        }

        /// <summary>
        /// Построить график с 4 каналов в диапазоне leftBorder - rightBorder
        /// </summary>
        /// <param name="leftBorder"></param>
        /// <param name="rightBorder"></param>
        public void MakeGraph_4_Canal_Observable_minTime(long leftBorder, long rightBorder)
        {
            long[,] data =Converter_Data_for_Livecharts.Minimize_Row_1(
                initdata.Get_Bordered_Row(leftBorder, rightBorder),
                size_min, initdata.Get_Length_Bordered_Row(leftBorder, rightBorder));
           // int size_data = initdata.Get_b();

            _labels = Converter_Data_for_Livecharts.Convert_data_X_for_Labels(data);
            int labels_data_length = _labels.GetUpperBound(0) + 1;

            ChartValues<ObservablePoint> val_1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_4 = new ChartValues<ObservablePoint>();


            for (int i = 0; i < labels_data_length-1; i++)
            {
                val_1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 1]
                });

                val_2.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 2]
                });
                val_3.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 3]
                });
                val_4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 4]
                });

            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Канал 1",
                    Values = val_1,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },

               new LineSeries
                {
                    Title = "Канал 2",
                    Values = val_2,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                 new LineSeries
                {
                    Title = "Канал 3",
                    Values = val_3,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                  new LineSeries
                {
                    Title = "Канал 4",
                    Values = val_4,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                     Fill = Brushes.Transparent,
                    PointGeometry = null

                }
            };
        }


        private void shift_line(long[,] dat, int number_CANAL) 
        {
            int ll = dat.Length / 15;

            for (int y = 1; y < ll - 1; y++)
            {
                if (maximum < dat[y, number_CANAL])
                {
                    maximum = dat[y, number_CANAL];
                }

                if (minimum > dat[y, number_CANAL])
                {
                    minimum = dat[y, number_CANAL];
                }

            }

            if ((maximum - minimum) < 200)
            {
                shift_grafh = 200;
                shift_grafh_ekg = 200;
            }
            else if ((maximum - minimum) > 500)
            {
                shift_grafh = -500;
                shift_grafh_ekg = 400;
            }
            else if ((maximum - minimum) > 1000)
            {
                shift_grafh = -5500;
                shift_grafh_ekg = 5500;
            }
            else
            {
                shift_grafh_ekg = 200;
                shift_grafh = -300;
            }
        }

        /// <summary>
        /// Построить график c 4 каналов + макс - мин
        /// </summary>
        public void MakeGraph_On_Canal_Max_Min(long leftBorder, long rightBorder)
        {

            long[,] data = Converter_Data_for_Livecharts.Minimize_Row_1(
            initdata.Get_Bordered_Row(leftBorder, rightBorder),
            size_min, initdata.Get_Length_Bordered_Row(leftBorder, rightBorder));

            _labels = Converter_Data_for_Livecharts.Convert_data_X_for_Labels(data);
            int labels_data_length = _labels.GetUpperBound(0) + 1;

            long[] row_3 = Converter_Data_for_Livecharts.Minimize_Row(initdata.Get_row3(), size_min);
           
            shift_line(data, initdata.REG);

            ChartValues<ObservablePoint> val_1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_4 = new ChartValues<ObservablePoint>();


            for (int i = 0; i < labels_data_length - 1; i++)
            {
                val_1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 1]
                });

                val_2.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 2]
                });
                val_3.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 3]
                });
                val_4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 4]
                });

            }
            //Считаем особые точки
            long[,] special_points_X_red_const = Function_additional.Return_Position_X_Sp_Points(special_Point_red_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_red_const = Function_additional.Return_Position_Y_Sp_Points(special_Point_red_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_red_diff = Function_additional.Return_Position_X_Sp_Points(special_Point_red_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_red_diff = Function_additional.Return_Position_Y_Sp_Points(special_Point_red_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_ik_const = Function_additional.Return_Position_X_Sp_Points(special_Point_IK_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_ik_const = Function_additional.Return_Position_Y_Sp_Points(special_Point_IK_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_ik_diff = Function_additional.Return_Position_X_Sp_Points(special_Point_IK_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_ik_diff = Function_additional.Return_Position_Y_Sp_Points(special_Point_IK_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));



            ChartValues<ObservablePoint> sp_B1_red_const = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_red_const = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_red_diff = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_red_diff = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_IK_const = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_IK_const = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_IK_diff = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_IK_diff = new ChartValues<ObservablePoint>();

            for (int i = 1; i < special_points_X_red_const.Length / 5 - 1; i++)
            {

                sp_B1_red_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_const[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_const[1, i])
                });

                sp_B2_red_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_const[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_const[2, i])
                });
            }
            for (int i = 1; i < special_points_X_red_diff.Length / 5 - 1; i++)
            {
                sp_B1_red_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_diff[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_diff[1, i])
                });

                sp_B2_red_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_diff[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_diff[2, i])
                });

            }
            for (int i = 1; i < special_points_X_ik_const.Length / 5 - 1; i++)
            {
                sp_B1_IK_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_const[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_const[1, i])
                });

                sp_B2_IK_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_const[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_const[2, i])
                });
            }
            for (int i = 1; i < special_points_X_ik_diff.Length / 5 - 1; i++)
            {
                sp_B1_IK_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_diff[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_diff[1, i])
                });

                sp_B2_IK_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_diff[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_diff[2, i])
                });

            }

            //Строим график
            SeriesCollection = new SeriesCollection
            {
                 new LineSeries
                {
                    Title = "Канал 1",
                    Values = val_1,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },

               new LineSeries
                {
                    Title = "Канал 2",
                    Values = val_2,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                 new LineSeries
                {
                    Title = "Канал 3",
                    Values = val_3,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                  new LineSeries
                {
                    Title = "Канал 4",
                    Values = val_4,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },

                  new ScatterSeries
                  {
                      Title = "B1-1",
                      Values = sp_B1_red_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-1",
                      Values = sp_B2_red_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },

                  new ScatterSeries
                  {
                      Title = "B1-2",
                      Values = sp_B1_red_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-2",
                      Values = sp_B2_red_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B1-3",
                      Values = sp_B1_IK_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-3",
                      Values = sp_B2_IK_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B1-4",
                      Values = sp_B1_IK_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-4",
                      Values = sp_B2_IK_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  }


            };
        }

        /// <summary>
        /// Построить график от канала ФПГ
        /// </summary>
        public void MakeGraph_On_Canal_FPG(long leftBorder, long rightBorder)
        {

            long[,] data = Converter_Data_for_Livecharts.Minimize_Row_1(
            initdata.Get_Bordered_Row(leftBorder, rightBorder),
            size_min, initdata.Get_Length_Bordered_Row(leftBorder, rightBorder));
           
            _labels = Converter_Data_for_Livecharts.Convert_data_X_for_Labels(data);
            int labels_data_length = _labels.GetUpperBound(0) + 1;

            long[] row_3 = Converter_Data_for_Livecharts.Minimize_Row(initdata.Get_row3(), size_min);
           
            shift_line(data, initdata.REG);
                     
            ChartValues<ObservablePoint> val_4 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_5 = new ChartValues<ObservablePoint>();

            for (int i = 0; i < labels_data_length-1; i++)
            {    
                val_4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, initdata.REG]
                });
                val_5.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = row_3[i] / 10 + shift_grafh
                });
            }
            //Считаем особые точки
            long[,] special_points_X = Function_additional.Return_Position_X_Sp_Points(special_Point.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y = Function_additional.Return_Position_Y_Sp_Points(special_Point.Get_Bordered_Spec_Point(leftBorder, rightBorder));

            ChartValues<ObservablePoint> sp_B1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B4 = new ChartValues<ObservablePoint>();

            for (int i = 0; i < special_points_X.Length/5-1; i++)
            {
                sp_B1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y[1, i])
                });

                sp_B2.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y[2, i])
                });
                sp_B3.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X[3, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y[3, i])
                });
                sp_B4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X[4, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y[4, i])
                });
            }


            //Строим график
            SeriesCollection = new SeriesCollection
            {                         
              
                  new LineSeries
                {
                    Title = "ФПГ",
                    Values = val_4,
                    LineSmoothness = LineSmouth,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    ScalesYAt = 0

                },
                  new LineSeries
                {
                    Title = "Производная ФПГ",
                    Values = val_5,
                     LineSmoothness = LineSmouth,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    ScalesYAt = 0

                },
                  new ScatterSeries
                  {
                      Title = "B1",
                      Values = sp_B1,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2",
                      Values = sp_B2,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B3",
                      Values = sp_B3,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B4",
                      Values = sp_B4,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  }


            };
        }

        /// <summary>
        /// Построить график насыщения
        /// </summary>
        /// <param name="Saturation_t"></param>
        /// <param name="Saturation_y1"></param>
        public void Make_Graph_Saturation(double[] Saturation_t, double[] Saturation_y1, int length_p)
        {           
            ChartValues<ObservablePoint> val_1 = new ChartValues<ObservablePoint>();

            for (int i = 0; i < length_p-1; i++)
            {
                if (!Double.IsNaN(Saturation_y1[i]))
                {
                    val_1.Add(new ObservablePoint
                    {
                        X = Saturation_t[i] / 1000.0,
                        Y = Saturation_y1[i]
                    });
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Сатурация",
                    Values = val_1,
                    LineSmoothness = LineSmouth,
                    Fill = Brushes.Transparent,
                    ScalesYAt = 0
                },
            };

        }

        public void MakeGraph_Saturation_Four_Canal(long leftBorder, long rightBorder, double[] Saturation_t, double[] Saturation_y1, int length_p)
        {

            long[,] data = Converter_Data_for_Livecharts.Minimize_Row_1(
            initdata.Get_Bordered_Row(leftBorder, rightBorder),
            size_min, initdata.Get_Length_Bordered_Row(leftBorder, rightBorder));

            _labels = Converter_Data_for_Livecharts.Convert_data_X_for_Labels(data);
            int labels_data_length = _labels.GetUpperBound(0) + 1;

            long[] row_3 = Converter_Data_for_Livecharts.Minimize_Row(initdata.Get_row3(), size_min);

            shift_line(data, initdata.REG);

            ChartValues<ObservablePoint> val_1 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_3 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> val_4 = new ChartValues<ObservablePoint>();


            for (int i = 0; i < labels_data_length - 1; i++)
            {
                val_1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 1]
                });

                val_2.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 2]
                });
                val_3.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 3]
                });
                val_4.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(data[i, 0]) / 1000000,
                    Y = data[i, 4]
                });

            }
            //Считаем особые точки
            long[,] special_points_X_red_const = Function_additional.Return_Position_X_Sp_Points(special_Point_red_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_red_const = Function_additional.Return_Position_Y_Sp_Points(special_Point_red_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_red_diff = Function_additional.Return_Position_X_Sp_Points(special_Point_red_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_red_diff = Function_additional.Return_Position_Y_Sp_Points(special_Point_red_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_ik_const = Function_additional.Return_Position_X_Sp_Points(special_Point_IK_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_ik_const = Function_additional.Return_Position_Y_Sp_Points(special_Point_IK_const.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_X_ik_diff = Function_additional.Return_Position_X_Sp_Points(special_Point_IK_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));
            long[,] special_points_Y_ik_diff = Function_additional.Return_Position_Y_Sp_Points(special_Point_IK_diff.Get_Bordered_Spec_Point(leftBorder, rightBorder));



            ChartValues<ObservablePoint> sp_B1_red_const = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_red_const = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_red_diff = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_red_diff = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_IK_const = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_IK_const = new ChartValues<ObservablePoint>();

            ChartValues<ObservablePoint> sp_B1_IK_diff = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> sp_B2_IK_diff = new ChartValues<ObservablePoint>();

            for (int i = 1; i < special_points_X_red_const.Length / 5 - 1; i++)
            {
                sp_B1_red_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_const[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_const[1, i])
                });

                sp_B2_red_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_const[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_const[2, i])
                });
            }
            for (int i = 1; i < special_points_X_red_diff.Length / 5 - 1; i++)
            {
                sp_B1_red_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_diff[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_diff[1, i])
                });

                sp_B2_red_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_red_diff[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_red_diff[2, i])
                });
            }
            for (int i = 1; i < special_points_X_ik_const.Length / 5 - 1; i++)
            {
                sp_B1_IK_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_const[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_const[1, i])
                });

                sp_B2_IK_const.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_const[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_const[2, i])
                });
            }
            for (int i = 1; i < special_points_X_ik_diff.Length / 5 - 1; i++)
            {
                sp_B1_IK_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_diff[1, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_diff[1, i])
                });

                sp_B2_IK_diff.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(special_points_X_ik_diff[2, i]) / 1000000,
                    Y = Convert.ToDouble(special_points_Y_ik_diff[2, i])
                });

            }

            ChartValues<ObservablePoint> val_saturation = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Saturation_t.Length-1; i++)
            {
                if (!Double.IsNaN(Saturation_y1[i]))
                {
                val_saturation.Add(new ObservablePoint
                {
                    X = Saturation_t[i]/1000.0,
                    Y = Saturation_y1[i]
                });
                }
                
            }


            //Строим график
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Сатурация",
                    Values = val_saturation,
                    Fill = Brushes.Transparent,
                    ScalesYAt = 1
                },

                 new LineSeries
                {
                    Title = "Канал 1",
                    Values = val_1,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },

               new LineSeries
                {
                    Title = "Канал 2",
                    Values = val_2,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                 new LineSeries
                {
                    Title = "Канал 3",
                    Values = val_3,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },
                  new LineSeries
                {
                    Title = "Канал 4",
                    Values = val_4,
                    LineSmoothness = LineSmouth,
                    ScalesYAt = 0,
                    Fill = Brushes.Transparent,
                    PointGeometry = null

                },

                  new ScatterSeries
                  {
                      Title = "B1-1",
                      Values = sp_B1_red_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-1",
                      Values = sp_B2_red_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },

                  new ScatterSeries
                  {
                      Title = "B1-2",
                      Values = sp_B1_red_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-2",
                      Values = sp_B2_red_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B1-3",
                      Values = sp_B1_IK_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-3",
                      Values = sp_B2_IK_const,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B1-4",
                      Values = sp_B1_IK_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  },
                  new ScatterSeries
                  {
                      Title = "B2-4",
                      Values = sp_B2_IK_diff,
                      ScalesYAt = 0,
                      StrokeThickness = 10.0
                  }

            };
        }

    }
}
