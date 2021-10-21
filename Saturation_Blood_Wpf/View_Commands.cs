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
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Forms;

namespace Saturation_Blood_Wpf
{
    public partial class View_Labels 
    {
        private delegate void Shift_data(int Left_, int Right_);
        Shift_data shift_;

        private CartesianChart mytestchart;

          public CartesianChart myTestChart
        {
            get { return mytestchart; }
            set
            {
                mytestchart = value;
                OnPropertyChanged();
            }
        }


        private Model_livecharts model;

        private int N_see = 10;

        public int N_SEE
        {
            get { return N_see; }
            set
            {
                N_see = value;
                OnPropertyChanged();
            }
        }

        #region 
        
        /// <summary>
        /// Открыть файл
        /// </summary>
        private Command_Graph open_file;

        public Command_Graph Open_File_Command
        {
            get
            {
                return open_file ??
                    (open_file = new Command_Graph(obj => {

                        Str = " ";
                        ZoomingMode = ZoomingOptions.X;
                        Step_Axis_Y_Left = 200.0;

                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();
                        model.Open_File_With_Data();
                        model.Inizilize_Base_Data();
                        model.Set_size_data(N_see);
                        model.Calculate_Base_Image();

                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Labels = model.Get_Labels_Model();
                        YFormatter = value => value.ToString("");
                        Axis_Left_Title = "arwargh";
                        Str = "Готово";
                  
                        //Axis_Left_Title = model.Get_Axis_Title_Left_Model();
                       // Axis_Right_Title = model.Get_Axis_Title_Right_Model();
                        //Is_Show_Labels = model.Get_Is_Show_Label_Model();

                    }));
            }
        }

        /// <summary>
        /// Сохранить график
        /// </summary>
        private Command_Graph save_graph;

        public Command_Graph Save_Graph_Command
        {
            get
            {
                return save_graph ??
                    (save_graph = new Command_Graph(obj => {
                        
                        ZoomingMode = ZoomingOptions.X;

                    }));
            }
        }

        /// <summary>
        /// Сохранить данные
        /// </summary>
        private Command_Graph save_data;

        public Command_Graph Save_Data_Command
        {
            get
            {
                return save_data ??
                    (save_data = new Command_Graph(obj => {

                        string adres = "q";
                        string datapath = "w";

                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            adres = fbd.SelectedPath;
                            datapath = Path.Combine(System.Windows.Forms.Application.StartupPath);
                            try
                            {
                                FileInfo f1 = new FileInfo(Path.Combine(System.Windows.Forms.Application.StartupPath, "test3.txt"));
                                if (f1.Exists)
                                {
                                    f1.CopyTo(Path.Combine(adres, "test.txt"), true);
                                }


                                FileInfo f8 = new FileInfo(Path.Combine(System.Windows.Forms.Application.StartupPath, "Насыщение.txt"));
                                if (f8.Exists)
                                {
                                    f8.CopyTo(Path.Combine(adres, "Насыщение.txt"), true);
                                }

                            }
                            catch {
                                System.Windows.MessageBox.Show("Аккуратнее переименовайте папку!");
                            }

                        }

                    }));
            }
        }

        /// <summary>
        /// Сдвиг влево
        /// </summary>
        private Command_Graph left_shift;

        public Command_Graph Left_Shift_Command
        {
            get
            {
                return left_shift ??
                    (left_shift = new Command_Graph(obj => {
                        Str = "";

                        Axis_X_Min_Value -= 10;
                        Axis_X_Max_Value -= 10;
                        shift_?.Invoke(Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));

                        Value_Slider = Axis_X_Min_Value;
                        
                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "U, напряжение";
                        Str = "Готово";

                    }));
            }
        }

        /// <summary>
        /// Сдвиг вправо
        /// </summary>
        private Command_Graph right_shift;

        public Command_Graph Right_Shift_Command
        {
            get
            {
                return right_shift ??
                    (right_shift = new Command_Graph(obj => {
                        Str = "";

                        Axis_X_Min_Value += 10;
                        Axis_X_Max_Value += 10;
                        shift_?.Invoke(Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));

                        Value_Slider = Axis_X_Min_Value;
                        
                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "U, напряжение";
                        Str = "Готово";


                    }));
            }
        }

        /// <summary>
        /// Очистить график
        /// </summary>
        private Command_Graph clear_graph;

        public Command_Graph Clear_Graph_Command
        {
            get
            {
                return clear_graph ??
                    (clear_graph = new Command_Graph(obj => {
                        Clear_data();
                        Step_Axis_Y_Left = 200.0;
                        ZoomingMode = ZoomingOptions.X;

                    }));
            }
        }

        /// <summary>
        /// Обновить
        /// </summary>
        private Command_Graph reset_data;

        public Command_Graph Reset_Data_Command
        {
            get
            {
                return reset_data ??
                    (reset_data = new Command_Graph(obj => {
                        
                        Clear_data();
                        Str = " ";
                        Step_Axis_Y_Left = 200.0;

                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();                      
                        model.Inizilize_Base_Data();
                        model.Set_size_data(N_see);
                        //  model.Calculate_Base_Image();
                        model.Calculate_Base_Image_With_Time_Limit(
                            Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));

                        shift_ = model.Calculate_Base_Image_With_Time_Limit;

                        SeriesCollection = model.Get_SeriesCollection_Model();                    
                        Axis_Left_Title = "U, напряжение";
                        Str = "Готово";
                        ZoomingMode = ZoomingOptions.X;

                        
                        Minimum_Value_Slider = 0;
                        Maximum_Value_Slider = model.Get_Signal_Length()/1000000;
                        Value_Slider = Axis_X_Min_Value;


                    }));
            }
        }

        /// <summary>
        /// Рассчитать точки макс-мин
        /// </summary>
        private Command_Graph calculate_max_min_data;

        public Command_Graph Calculate_Max_Min_Command
        {
            get
            {
                return calculate_max_min_data ??
                    (calculate_max_min_data = new Command_Graph(obj => {


                        Clear_data();
                        Str = " ";
                        Step_Axis_Y_Left = 200.0;

                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();
                        model.Inizialize_Base_Data_for_max_min();

                        //  model.Calculate_Base_Image();
                        model.Calculate_Max_Min_Diff();
                        model.Calculate_Max_Min_Const();
                        model.Inizialize_Graph_Max_Min();
                        model.Set_size_data(N_see);
                        model.Calculate_max_min_Image_With_Time_Limit(
                            Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));

                        shift_ = model.Calculate_max_min_Image_With_Time_Limit;

                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "U, напряжение";
                        Str = "Готово";

                        int[] l = model.Get_Length_Special_Point_four_Canal();

                        RED_CONST_N_Special_points = $"Особых точек: {l[0]}";
                        RED_DIFF_N_Special_points = $"Особых точек: {l[1]}";
                        IK_CONST_N_Special_points = $"Особых точек: {l[2]}";
                        IK_DIFF_N_Special_points = $"Особых точек: {l[3]}";

                        ZoomingMode = ZoomingOptions.X;

                        Minimum_Value_Slider = 0;
                        Maximum_Value_Slider = model.Get_Signal_Length() / 1000000;

                    }));
            }
        }

        /// <summary>
        /// Рассчитать особые точки с одного канала
        /// </summary>
        private Command_Graph calculate_special_point_one_canal;

        public Command_Graph Calculate_Special_Point_One_Canal_Command
        {
            get
            {
                return calculate_special_point_one_canal ??
                    (calculate_special_point_one_canal = new Command_Graph(obj => {

                        Clear_data();
                        Str = " ";
                        Step_Axis_Y_Left = 200.0;

                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();
                        model.Inizilize_Base_Data_for_Special_Point();
                        
                        //  model.Calculate_Base_Image();
                        model.Calculate_One_Canal_Image_With_Time_Limit_one();
                        model.Inizilize_Special_Point();
                        model.Calculate_Special_Points();
                        model.Inizialize_Graph_Sp_Point();
                        model.Set_size_data(N_see);
                        model.Calculate_One_Canal_Image_With_Time_Limit_two(
                            Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));
                       

                        shift_ = model.Calculate_One_Canal_Image_With_Time_Limit_two;

                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "U, напряжение";
                        Str = "Готово";
                        ZoomingMode = ZoomingOptions.X;

                        Minimum_Value_Slider = 0;
                        Maximum_Value_Slider = model.Get_Signal_Length() / 1000000;

                    }));
            }
        }

        /// <summary>
        /// Рассчитать сатурацию
        /// </summary>
        private Command_Graph calculate_saturation;

        public Command_Graph Calculate_Saturation_Command
        {
            get
            {
                return calculate_saturation ??
                    (calculate_saturation = new Command_Graph(obj => {

                        Step_Axis_Y_Left = 200.0;
                        Clear_data();
                        Str = " ";
                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();
                        model.Inizialize_Base_Data_for_max_min();

                        //  model.Calculate_Base_Image();
                        model.Calculate_Max_Min_Diff();
                        
                        model.Calculate_Reflection();

                        model.Calculate_Max_Min_Const();
                        model.Inizialize_Graph_Max_Min();

                        model.Set_size_data(N_see);
                        
                        model.Inizilize_Saturation();
                        model.Calculate_Saturation();

                        model.Make_Graph_Saturation_Four_Canal(Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));
                        model.Save_Saturation();

                        shift_ = model.Make_Graph_Saturation_Four_Canal;

                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "U, напряжение";
                        Axis_Right_Title = "Насыщение, %";
                        Str = "Готово";

                        int[] l = model.Get_Length_Special_Point_four_Canal();

                        RED_CONST_N_Special_points = $"Особых точек: {l[0]}";
                        RED_DIFF_N_Special_points = $"Особых точек: {l[1]}";
                        IK_CONST_N_Special_points = $"Особых точек: {l[2]}";
                        IK_DIFF_N_Special_points = $"Особых точек: {l[3]}";

                        saturation_data = model.Get_Saturation_Data();

                        ZoomingMode = ZoomingOptions.X;

                        Minimum_Value_Slider = 0;
                        Maximum_Value_Slider = model.Get_Signal_Length() / 1000000;

                    }));
            }
        }
               

        /// <summary>
        /// Рассчитать только сатурацию
        /// </summary>
        private Command_Graph calculate_only_saturation;

        public Command_Graph Calculate_Only_Saturation_Command
        {
            get
            {
                return calculate_only_saturation ??
                    (calculate_only_saturation = new Command_Graph(obj => {
                        Clear_data();
                        Str = " ";
                        Step_Axis_Y_Left = 0.2;
                        Step_Axis_X = 30.0;

                        model = new Model_livecharts(textbox_data, radbutton_settings_data, special_point_n);
                        model.Set_all_inner_parameters();
                        model.Inizialize_Base_Data_for_max_min();

                        //  model.Calculate_Base_Image();
                        model.Calculate_Max_Min_Diff();
                        model.Calculate_Reflection();

                        model.Calculate_Max_Min_Const();
                        model.Inizialize_Graph_Max_Min();
                        model.Set_size_data(N_see);
  
                        model.Inizilize_Saturation();
                        model.Calculate_Saturation();

                        model.Make_Graph_Saturation();
                        model.Save_Saturation();

                        SeriesCollection = model.Get_SeriesCollection_Model();
                        Axis_Left_Title = "Насыщение, %";
                        
                        int[] l = model.Get_Length_Special_Point_four_Canal();

                        RED_CONST_N_Special_points = $"Особых точек: {l[0]}";
                        RED_DIFF_N_Special_points = $"Особых точек: {l[1]}";
                        IK_CONST_N_Special_points = $"Особых точек: {l[2]}";
                        IK_DIFF_N_Special_points = $"Особых точек: {l[3]}";

                        saturation_data = model.Get_Saturation_Data();

                        ZoomingMode = ZoomingOptions.X;

                        Str = "Готово";

                    }));
            }
        }

        #endregion

        public void Clear_data()
        {
            Axis_Right_Title = "";
            Is_Show_Labels = false;

            if (SeriesCollection != null)
            {
                SeriesCollection.Clear();
            }
            else
            {

            }

        }




    }
}
