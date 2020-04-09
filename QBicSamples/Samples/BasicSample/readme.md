# Introduction
The **BasicSample** folder is the basic method of creating a view, with add, edit and delete functionality.

The classes in this folder correspond more or less to CRUD functionality.

Generally though, when adding CRUD functionality using QBic, one would start with a **View** screen, from where it would be possible add new items.

The different classes are explained in more detail below:

## SampleView
SampleView.cs inherits from **CoreView**, which inherits from **ShowView** that is the most primitive class to inherit from for a view screen.

When creating a **CoreView** class, the following methods should generally be implemented:

### AllowInMenu
Setting this value to true, will a menu item to be added which will show this view when clicked. If unimplemented, this value defaults to **false**.

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).

### ConfigureColumns
This method is used to set the columns that will be added to the view.
The **columnConfig** parameter can be added to using multiple utility methods such as 
 > columnConfig.AddStringColumn("Column Label", "ColumnName");
 Where "Column Label" is the label that is displayed in the view, and ColumnName is the value of the field that is returned by the **GetData** method.

### GetData
This method should return the data to be shown for the **current** view only.
This method is passed a **settings** parameter which contains values to specify the number of records per page, the current page, a filter value, etc.
These values should be used to return the relevant records, where the field names returned match the "ColumnName" values as is set by the **ConfigureColumns* function.

### GetDataCount
This method needs to return the total number of records that exist for the view. 
This number is used to calculate how many pages there are based on the number of items to display per page.

        
### GetId
This method should return a MenuNumber/EventNumber that uniquely identifies this view.
Each action/event/menu item in QBic has a unique menu or event number that is used to link an action with the code to execute.

### GetViewMenu
This is an optional field, which does not need to be overridden.
If overridden, this adds buttons near the top of the view, and are only visible when this view is shown.
An example of when this might be used is to create an "Add" button to add a new item, or a "Back" button, which can go back to a previously shown view.

### CreateQuery

### GetAliases
        
### GetEventParameters

### GetFilterItems

### GetNonStringFilterItems

### GetViewParameters

### OrderQuery

### TransformData





## SampleModify
SampleModify.cs inherits from **CoreModify** that inherits from **GetInput**, which is the most primitive class to inherit from for an input screen.
In most cases, adding a new item is very similar to modifying an existing item, and it is usually easiest to create a common **abstract** class that is responsible for both adding and editing a record.
The following methods should be implemented for a "CoreModify" class

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).

### Initialize
This is an optional override, and is called first when the add or edit (modify) action has been triggered.

### GetInputFields

### CreateDefaultItem
       
### GetInputParameters

### GetParameterToPassToView

### GetViewNumber

### InputFields

### PerformModify

### ProcessAction

### ErrorMessage

### GetParameter


## SampleAdd
SampleAdd.cs inherits from **CoreModify** that inherits from **GetInput**, which is the most primitive class to inherit from for an input screen.
In most cases, adding a new item is similar to modifying an existing item, and it is usually easiest to create a common **abstract** class that is responsible for both adding and editing a record.
The following methods should be implemented for a "CoreModify" class

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).


### CreateDefaultItem

### GetInputFields

### GetInputParameters

### GetParameterToPassToView

### GetViewNumber

### Initialize
This is an optional override, and is called first when the add or edit (modify) action has been triggered.

### InputFields

### PerformModify

### ProcessAction

### ErrorMessage

### GetParameter

## SampleEdit
SampleEdit.cs inherits from **CoreModify** that inherits from **GetInput**, which is the most primitive class to inherit from for an input screen.
In most cases, editing a new item is similar to modifying an existing item, and it is usually easiest to create a common **abstract** class that is responsible for both adding and editing a record.
The following methods should be implemented for a "CoreModify" class

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).


### CreateDefaultItem

### GetInputFields

### GetInputParameters

### GetParameterToPassToView

### GetViewNumber

### Initialize
This is an optional override, and is called first when the add or edit (modify) action has been triggered.

### InputFields

### PerformModify

### ProcessAction

### ErrorMessage

### GetParameter

## SampleDelete
SampleEdit.cs inherits from **CoreDeleteAction** that inherits from **CoreAction**, which is the most primitive class to inherit from for an action.
The following methods should be implemented for a "CoreDeleteAction" class

### EntityName

### Description

### ViewNumber

### ParametersToPassToView

### DeleteOtherItems

### ProcessAction

### ErrorMessage