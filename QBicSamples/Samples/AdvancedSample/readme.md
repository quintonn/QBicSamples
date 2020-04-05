AdvancedAdd**, **AdvancedDelete**, **AdvancedEdit**, **AdvancedModify** and **AdvancedView** classes from **AdvancedSample** are used to make actions with data entities of the project.

**AdvancedModify** - class for Add or Edit entity

**AdvancedView** - class for view entities

**AdvancedDelete** - class for delete entity



**AdvancedModify** class has following main methods:

*GetInputFields()* is a method that return a list of input fields where a user can input data.

*ProcessAction()* method performs some operations and validation on data and save data to a database. Parameter *actionNumber* has int value. 
If user clicks 'Cancel', value of *actionNumber* will be 1 and if click on 'Submit', value of *actionNumber* will be 0.



**AdvancedView** class is using to show data on the screen. Main methods of SampleView class are:

*ConfigureColumns()* method allow to configure number of columns in table and type and value of data for every column.

*GetData()* method allow user to prepare the data collection from database.

*TransformData()* method allow user to make some transformation on data collection from database.

*GetDataCount()* method count number of records that was retreive from database.

*CreateQuery()* method query the database in order to retreive data.