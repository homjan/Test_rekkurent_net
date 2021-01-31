using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rekkurent_net.layer_neural_network
{
    public interface IRekkurent_Cell_Element
    {

        void Read_from_file_bias_1(String name_file);
        void Read_from_file_weight_1(String name_file);
        void Read_from_file_state_Matrix(String name_file);
        void Divide_All();
        void Calculate_Full_Layer(double[] x);
        double[] Get_output_Result();

        

    }
}
