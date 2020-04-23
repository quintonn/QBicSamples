# Introduction
The **AdvancedSample** folder is the most complicate method of creating a view, with add, edit and delete functionality.

The classes in this folder correspond more or less to CRUD functionality.

Generally though, when adding CRUD functionality using QBic, one would start with a **View** screen, from where it would be possible add new items.

The different classes are explained in more detail below:

## AdvancedView
AdvancedView inherits from **ShowView**, which is the most primitive class to inherit from for a view screen.

When creating a **ShowView** class, the following methods should generally be implemented:

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

## AdvancedModify
AdvancedModify inherits from **GetInput**, which is the most primitive class to inherit from for an input screen.
In most cases, adding or editing a new item is very similar to modifying an existing item, and it is usually easiest to create a common **abstract** class that is responsible for both adding and editing a record.
The following methods should be implemented for a "GetInput" class

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).

### Initialize
This is an optional override, and is called first when the add or edit (modify) action has been triggered.

### GetInputFields
This returns the input fields that will added to the input screen.

### OnPropertyChanged
This is an optional override, and is called for any input that has it's **RaisePropertyChangedEvent** value set to true, and will be called when the user modified that input.  
This can be used to update input fields' visibility as well as update a combo box's contents based on inputs selected by a user.

### ProcessAction
This is called when the user submits or cancels an input screen, or presses any other button on the input screen.  
The button ID is passed to this function, and any input data can be obtained using the utility function **GetValue** or directly from the **InputData** field.

## AdvancedAdd
AdvancedAdd inherits from **AdvancedModify**.  
In order to specify that this instance of the **AdvancedModify** is used for adding, the following needs to be implemented:

### Constructor
The base contructor has a boolean value for **isNew** which indicates if this is for a new item or not, and in this case we pass **true**.

### GetId
This is the unique identifier for this input screen.

## AdvancedEdit
AdvancedEdit inherits from **AdvancedModify**.  
In the same way we set different values for AdvancedAdd, we have to set values for AdvancedEdit, but this time we set values specific for a modify, or edit.

### Constructor
The base contructor has a boolean value for **isNew** which indicates if this is for a new item or not, and in this case we pass **false**.

### GetId
This is the unique identifier for this input screen.

## AdvancedDetails
AdvancedDetails inherits from **DoSomething**, which is the most primitive class to inherit in order to process a user action, which usually relates to a menu button click or clicking a link in a view, as in this case.  

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.  
In this case it is set to false, as this view should only be shown from another screen.  

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).

### GetId
This is the unique identifier for this input screen.

### ProcessAction
This code is invoked when the user invokes the button or action associated with the value returned by **GetId**.

## AdvancedDelete
AdvancedDelete inherits from **DoSomething**, which is used to handle a user event typically.

### AllowInMenu
This has the same function as before, and is generally set to false, unless the input screen should be allowed to be initialized directly from the main menu, rather than from a view menu or view action.  
In this case it is set to false, as this view should only be shown from another screen.  

### Description
The value in this field is what is displayed when adding a menu, and is also the default Title of the view (which can be overridden also).

### GetId
This is the unique identifier for this input screen.

### ProcessAction
This code is invoked when the user invokes the button or action associated with the value returned by **GetId**.