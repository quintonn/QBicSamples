SampleAdd**, **SampleDelete**, **SampleEdit**, **SampleModify** and **SampleView** classes from **BasicSamples** are used to make actions with data entities of the project.

**SampleModify** - class for Add or Edit entity

**SampleView** - class for view entities

**SampleDelete** - class for delete entity



**SampleModify** class has following main methods:

*InputFields()* is a method that return a list of input fields where a user can input data.

*PerformModify()* method performs some operations and validation on data and save data to a database. Parameter *isNew* has boolean value. 
If we add new data, value of *isNew* will be true but if we are editing existing data, value of *isNew* will be false.



**SampleView** class is using to show data on the screen. Main methods of SampleView class are:

***ConfigureColumns()*** method allow to configure number of columns in table and type and value of data for every column.

*GetFilterItems()* method allow to configure filters for columns.



