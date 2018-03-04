# MLDataPrepeparationTool
This is WinForms C# project which can import any textual data set, and transform it in to ML ready Training and Testing data sets, with full support of Numerical, Binary and Category encoding, defining features and label, Data normalization and handling Missing values.
Beside general export options, the Tool supports CNTK format.

![ML Data Preparation Tool](https://bhrnjica.files.wordpress.com/2018/03/2018-03-01_9-56-25.png?w=604&h=231) 

# System Requirements
In order to use the Tool, .NET Framework 4.7.1 should be installed.

# How to use the Tool
1 Load any text data into ML Preparation Tool, by pressing Import Data button, the Import dialog will appear, by  providing guidance to successfully import data into the Tool,

2 Transform the data by providing the following:

| Column option  | Suboptions | Description|
| ------------- | ------------- |  --------|
| Name          | xi, y         |In case the header is not provided in the imported data, automatic column names is generated.|
| Type          | Numeric       |Indicates the column is cominuous numeric value.|
|               | Binary        |Idicated the column data is binary with ony two posible values e.g. (male, femail)|
|               | Category      |Indicates the column data is categorical with more than two values. e.g. (R,G,B)|
|               | String        |The column will be ignore during export.|
| Encoding      ||In case of Binary and Category column type, the encoding must be defined.|
|               |(0,1)|First binary values will be 0, and second binary values will be 1.|
|               |(-1,1)|First binary values will be -1, and second binary values will be 1.|
|               |N|Category Level where each class treats as numeric value. In case of 3 categories(R,G, B), encoding will be (0,1,2)|
|               |1:N|Category representation with One-Hot vector with N columns. In case of 3 categories(R,G, B), encoding will be R =  (1,0,0),G =  (0,1,0), B =  (0,0,1)|
|               |1:N-1(0)|Category representation with dummy coding with N-1 columns. In case of 3 categories(R, G, B), encoding will be R =  (1,0),G =  (0,1), B =  (0,0)|
|               |1:N-1(-1)|Category representation with dummy coding with N-1 columns. In case of 3 categories(R, G, B), encoding will be R =  (1,0),G =  (0,1), B =  (-1,-1)|
| Variable      | Input         |The column will be treated as feature during export.|
|               | Output        |The column will be treated as label during export|
|               | Ignore        |The column will be ignore during export.|
| Scaling       | None          |No scaling will be performed during export.|
|               | MinMax        |MinMax normalisation will be performed during export.|
|               | Gauss         |Gauss standardization will be performed during export. |
| Missing Value |               |defines the replacement for the missing value withing the column. There are several options related to numeric and two options (Random and Mode ) for categorical type.|
|               | Ignore           |In case the missing value whole row will be ommited during export.|
|               | Average          |Missing value will be replaces with column average value.|
|               | Max              |Missing value will be replaces with column max value.|
|               | Min              |Missing value will be replaces with column min value.|
|               | Mode             |Missing value will be replaces with column mode value.|
|               | Random           |Usialy good for binary and Categorical columns. Missing value will be replaces with random value.|



More information can be found at https://bhrnjica.net/tag/mldataprep/ 
