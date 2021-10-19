using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Helpers;

namespace Saturation_Blood_Wpf
{
    public partial class View_Labels : INotifyPropertyChanged
    {
        private string str;
        public string Str
        {
            get { return str; }
            set
            {
                str = value;
                OnPropertyChanged();
            }
        }


        //fields
        #region

        /// <summary>
        /// Настройки графопостроителя
        /// </summary>
        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged();
            }
        }
        public Func<double, string> YFormatter { get; set; }
        public double Step_Y { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public int N_points;

        

        private ZoomingOptions _zoomingMode;

        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Задание оси x
        /// </summary>
        private double axis_X_min_value = 0;
        public double Axis_X_Min_Value
        {
            get { return axis_X_min_value; }
            set { axis_X_min_value = value;
                OnPropertyChanged();
            }
        }

        private double axis_X_max_value = 10;
        public double Axis_X_Max_Value
        {
            get { return axis_X_max_value; }
            set { axis_X_max_value = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Задание оси справа
        /// </summary>
        private string axis_right_title;

        public string Axis_Right_Title
        {
            get { return axis_right_title; }
            set { axis_right_title = value; }
        }

        private string axis_left_title = "Амплитуда";

        public string Axis_Left_Title
        {
            get { return axis_left_title; }
            set { axis_left_title = value; }
        }

        private bool is_show_labels;

        public bool Is_Show_Labels
        {
            get { return is_show_labels; }
            set { is_show_labels = value; }
        }

        private double step_axis_Y_left = 200;

        public double Step_Axis_Y_Left 
        {
            get { return step_axis_Y_left; }
            set { step_axis_Y_left = value; }
        }

        private Double step_axis_X = 5;

        public double Step_Axis_X
        {
            get { return step_axis_X; }
            set { step_axis_X = value; }
        
        }

        #endregion
        ////////////////////////////////////////////////////////////////
        /// Settings rudbutton
        ////////////////////////////////////////////////////////////////
        ///

        #region
        private Dictionary<string, bool> radbutton_settings_data = new Dictionary<string, bool>(7)
        {
              {"base_algorithm_rud_", true},
               {"neural_net_algorithm_rud_", false},
               {"statistic_algorithm_rud_", false},
               {"const_calculation_only_", false},
               {"const_and_dif_calculation_only_", true},
               {"calculate_const_on_diff_base_", true},
               {"invert_const_data_", true}

        };


        private bool base_algorithm_rud = true;
        public bool Base_Algorithm_Rud
        {
            get { return base_algorithm_rud; }
            set {
                base_algorithm_rud = value;
                radbutton_settings_data["base_algorithm_rud_"] = value;
                OnPropertyChanged();
            }

        }

        private bool neural_net_algorithm_rud = false;
        public bool Neural_Net_Algorithm_Rud
        {
            get { return neural_net_algorithm_rud; }
            set
            {
                neural_net_algorithm_rud = value;
                radbutton_settings_data["neural_net_algorithm_rud_"] = value;
                OnPropertyChanged();
            }

        }

        private bool statistic_algorithm_rud = false;
        public bool Statistic_Algorithm_Rud
        {
            get { return statistic_algorithm_rud; }
            set
            {
                statistic_algorithm_rud = value;
                radbutton_settings_data["statistic_algorithm_rud_"] = value;
                OnPropertyChanged();
            }

        }

        private bool const_calculation_only = false;

        public bool Const_Calculation_Only 
        {
            get { return const_calculation_only; }
            set { const_calculation_only = value;
                radbutton_settings_data["const_calculation_only_"] = value;
                OnPropertyChanged();

            }
        
        }

        private bool const_and_dif_calculation_only = true;

        public bool Const_And_Diff_Calculation_Only
        {
            get { return const_and_dif_calculation_only; }
            set { const_and_dif_calculation_only = value;
                radbutton_settings_data["const_and_dif_calculation_only_"] = value;
                OnPropertyChanged();
            }

        }

        private bool calculate_const_on_diff_base = true;
        
        public bool Calculate_Const_On_Diff_Base {
            get { return calculate_const_on_diff_base; }
            set { calculate_const_on_diff_base = value;
                radbutton_settings_data["calculate_const_on_diff_base_"] = value;
                OnPropertyChanged();
            }
        
        }

        private bool invert_const_data = true;
        public bool Invert_Const_Data {
            get { return invert_const_data; }
            set { invert_const_data = value;
                radbutton_settings_data["invert_const_data_"] = value;
                OnPropertyChanged();
            }
        
        }
        #endregion

        /// <summary>
        /// Коэффициенты и сглаживание   
        /// </summary>
        /// 
        #region
        private Dictionary<string, string> textbox_data = new Dictionary<string, string>(14)
        {
            {"attenjuator_", "0,1"},
            {"k_pow_red_", "12"},
            {"k_pow_ik_", "22,6"},
            {"N_sl_", "3"},           
            {"red_const_", "2"},
            {"red_diff_", "1"},
            {"ik_const_", "3"},
            {"ik_diff_", "4"},
            {"red_const_regul_", "10"},
            {"red_diff_regul_", "50"},
            {"ik_const_regul_", "10"},            
            {"ik_diff_regul_", "90"},
            {"canal_FPG_", "1"},
            {"FPG_regul_", "90"}
        
        };
               
        private string attenjuator="0,1";
        public string Attenjuator {

            get { return attenjuator; }
            set { 
                attenjuator = value;
                textbox_data["attenjuator_"] = value;
                OnPropertyChanged();
            }
        }

        private string k_pow_red = "12";
        public string K_Pow_Red
        {
            get { return k_pow_red; }
            set { 
                k_pow_red = value;
                textbox_data["k_pow_red_"] = value;
                OnPropertyChanged();
            }
        }

        private string k_pow_ik = "22,6";
        public string K_Pow_IK {
            get { return k_pow_ik; }
            set {
                k_pow_ik = value;
                textbox_data["k_pow_ik_"] = value;
                OnPropertyChanged();
            }
        }

        private string N_sl = "3";
        public string N_SL
        {
            get { return N_sl; }
            set {
                N_sl = value;
                textbox_data["N_sl_"] = value;
                OnPropertyChanged();
            }
        
        }
       
        /// <summary>
        /// Вспомогательная регулировка
        /// </summary>
        private string canal_FPG = "1";

        public string CANAL_FPG 
        {
            get { return canal_FPG; }
            set {
                canal_FPG = value;
                textbox_data["canal_FPG_"] = value;
                OnPropertyChanged();
            }        
        }

        private string FPG_regul = "90";
        public string FPG_REGUL {
            get { return FPG_regul; }
            set { 
                FPG_regul = value;
                textbox_data["FPG_regul_"] = value;
                OnPropertyChanged();
            }
        
        }
        /// <summary>
        /// Основная регулировка
        /// </summary>
        /// 
        private string red_const = "2";
        public string RED_CONST {
            get { return red_const; }
            set { 
                red_const = value;
                textbox_data["red_const_"] = value;
                OnPropertyChanged();
            }
        }

        private string red_diff = "1";
        public string RED_DIFF
        {
            get { return red_diff; }
            set
            {
                red_diff = value;
                textbox_data["red_diff_"] = value;
                OnPropertyChanged();
            }
        }

        private string ik_const = "3";
        public string IK_CONST
        {
            get { return ik_const; }
            set
            {
                ik_const = value;
                textbox_data["ik_const_"] = value;
                OnPropertyChanged();
            }
        }

        private string ik_diff = "4";
        public string IK_DIFF
        {
            get { return ik_diff; }
            set
            {
                ik_diff = value;
                textbox_data["ik_diff_"] = value;
                OnPropertyChanged();
            }
        }

        private string red_const_regul = "10";
        public string RED_CONST_Regul {
            get { return red_const_regul; }
            set { 
                red_const_regul = value;
                textbox_data["red_const_regul_"] = value;
                OnPropertyChanged();
            }
        
        }

        private string ik_const_regul = "10";
        public string IK_CONST_Regul
        {
            get { return ik_const_regul; }
            set {
                ik_const_regul = value;
                textbox_data["ik_const_regul_"] = value;
                OnPropertyChanged();
            }

        }

        private string red_diff_regul = "50";
        public string RED_DIFF_Regul
        {
            get { return red_diff_regul; }
            set
            {
                red_diff_regul = value;
                textbox_data["red_diff_regul_"] = value;
                OnPropertyChanged();
            }

        }

        private string ik_diff_regul = "90";
        public string IK_DIFF_Regul
        {
            get { return ik_diff_regul; }
            set
            {
                ik_diff_regul = value;
                textbox_data["ik_diff_regul_"] = value;
                OnPropertyChanged();
            }

        }
        #endregion

        /// <summary>
        /// special point
        /// </summary>
        /// 
        #region

        private Dictionary<string, string> special_point_n = new Dictionary<string, string>(4) {
            {"red_const_n_", "Особых точек: 0"},
            {"red_diff_n_", "Особых точек: 0"},
             {"ik_const_n_", "Особых точек: 0"},
            {"ik_diff_n_", "Особых точек: 0"}

        };

        private string red_const_n_special_points = "Особых точек: 0";
        public string RED_CONST_N_Special_points
        {
            get { return red_const_n_special_points; }
            set
            {
                red_const_n_special_points = value;
                special_point_n["red_const_n_"] = value;
                OnPropertyChanged();
            }

        }

        private string ik_const_n_special_points = "Особых точек: 0";
        public string IK_CONST_N_Special_points
        {
            get { return ik_const_n_special_points; }
            set
            {
               ik_const_n_special_points = value;
                special_point_n["ik_const_n_"] = value;
                OnPropertyChanged();
            }

        }

        private string red_diff_n_special_points = "Особых точек: 0";
        public string RED_DIFF_N_Special_points
        {
            get { return red_diff_n_special_points; }
            set
            {
                red_diff_n_special_points = value;
                special_point_n["red_diff_n_"] = value;
                OnPropertyChanged();
            }

        }

        private string ik_diff_n_special_points = "Особых точек: 0";
        public string IK_DIFF_N_Special_points
        {
            get { return ik_diff_n_special_points; }
            set
            {
                ik_diff_n_special_points = value;
                special_point_n["ik_diff_n_"] = value;
                OnPropertyChanged();
            }

        }

        #endregion
        ////////////////////////////////////////////
        ///slider settings
        ////////////////////////////////////
        ///

        #region
        private Dictionary<string, double> slider_settings = new Dictionary<string, double>(3) {
            {"minimun_value_slider_", 0.0},
            {"maximum_value_slider_", 100.0},
            {"value_slider_", 0.0}

        };


        private double minimum_value_slider = 0.0;
        public double Minimum_Value_Slider {
            get { return minimum_value_slider; }
            set { minimum_value_slider = value;
                slider_settings["minimun_value_slider_"] = value;
                OnPropertyChanged();
            }
        }

        private double maximum_value_slider = 100.0;
        public double Maximum_Value_Slider
        {
            get { return maximum_value_slider; }
            set {
                maximum_value_slider = value-10;
                slider_settings["maximum_value_slider_"] = value-10;
                OnPropertyChanged();
            }
        }

        private double value_slider = 0.0;
        public double Value_Slider
        {
            get { return value_slider; }
            set { value_slider = value;
                slider_settings["value_slider_"] = value;

                Str = "";

                Axis_X_Min_Value = value;
                Axis_X_Max_Value = value +10;
                shift_?.Invoke(Convert.ToInt32(axis_X_min_value), Convert.ToInt32(axis_X_max_value));

                
                SeriesCollection = model.Get_SeriesCollection_Model();
                Axis_Left_Title = "U, напряжение";
                Str = "Готово";

                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// textbox settings - data
        /// </summary>

        private string[] saturation_data = { "0" };

        public string[] Saturation_Data 
        {
            get { return saturation_data; }
            set {
                saturation_data = value;
            }
        
        }
        



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ///////////////////////////////////////////////////////////////////
        ///

     

    }
}
