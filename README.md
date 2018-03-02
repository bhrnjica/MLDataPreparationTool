# MLDataPrepeparationTool
This is WinForms C# project which can import any txtual dataset, and transfom it in to ML ready Training and Testing data sets, with full support of Numerical, Binary and Category encoding, defining features and label, Data normalisation and handling Missing values.
Beside general export options, the Tool supports CNTK format.

![ML Data Preparation Tool](https://bhrnjica.files.wordpress.com/2018/03/2018-03-01_9-56-25.png?w=604&h=231) 

# System Requirements
In order to use the Tool, .NET Framework 4.7.1 should be installed.

# How to use the Tool
1 Load any taxt data into ML Preparation Tool, by pressing Import Data button, the Import dialog will apear which can provide tha data will be imported corectly in to the Tool,

2 Transform the data by providing the folowing:
 * Type – each column can be:
    - Numeric – which holds continuous numeric values,
    - Binary – which indicates two class categorical data,
    - Category – which indicates categorical data with more than two classes,
    - String – which indicate the column will not be part of training and testing data set,
  * Encoding – in case of Binary and Category column type, the encoding must be defined. The flowing encoding is supported:
    * Binary Encoding with (0,1) – first binary values will be 0, and second binary values will be 1.
    * Binary encoding with (-1,1) – first binary values will be -1, and second binary values will be 1.
    * Category Level- which each class treats as numeric value. In case of 3 categories(R,G, B), encoding will be (0,1,2)
    * Category 1:N- implements One-Hot vector with N columns. In case of 3 categories(R,G, B), encoding will be R =  (1,0,0),G =  (0,1,0), B =  (0,0,1).
    * Category 1:N-1(0) – implements dummy coding with N-1 columns. In case of 3 categories(R, G, B), encoding will be R =  (1,0),G =  (0,1), B =  (0,0).
    * Category 1:N-1(-1) – implements dummy coding with N-1 columns. In case of 3 categories(R, G, B), encoding will be R =  (1,0),G =  (0,1), B =  (-1,-1).
  * Variable – defines features and label. Only one label, and at least one features can be defined. Also the column can be defined as Ignore variable, which will skip that column.  The following options are sported:
    * Input – which identifies the column as feature or predictor,
    * Output – which identifies the column as label or model output.
 * Scaling – defines column scaling. Two scaling options are supported:
    * MinMax,
    * Gauss Standardization,
  Missing Values – defines the replacement for the missing value withing the column. There are several options related to numeric and two options (Random and Mode ) for categorical type.



More ifnormation can be found at https://bhrnjica.net/tag/mldataprep/ 
