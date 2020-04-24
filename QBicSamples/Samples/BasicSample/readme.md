# Introduction
The **BasicSample** folder is the basic method of creating a view, with add, edit and delete functionality.

The classes in this folder correspond more or less to basic CRUD functionality.

Generally though, when adding CRUD functionality using QBic, one would start with a **View** screen, from where it would be possible add new items.

The different classes are explained in more detail below:

## SampleView
SampleView is an **abstract** class that inherits from **CoreView**, which can be used to simplify creating data, or screen, views.

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

### GetFilterItems
This method returns the properties on the entity that will be used to filter out records when a user performs a search.
To filter on no fields, return something similar to the following, replacing **SampleModel** with your entity type:
> return new List<Expression<Func<SampleModel, object>>>();

## SampleModify
SampleModify inherits from **CoreModify**, which can be used to easily create an input screen for modifying an entity.  
This class provide the basics to obtain user input and then either create a new entity, or modify an existing record.  

**CoreModify** as the following methods and fields that need to be implemented:  

### EntityName
This is the name of the entity that is used to create titles on input screens and views.  

### GetViewNumber
This is the id, or event number for the corresponding view that will be shown after this screen is closed or cancelled.

### InputFields
This returns the input fields that will added to the input screen.  

### PerformModify
This is the code that is invoked when the user submits the input screen, either for adding a new record, or modifying an existing record.  
The input parameter **isNew** indicates if this is a new record or not.  
For relatively simple or straight forward cases, code very similar to what is provided in this sample can be used.  

## SampleAdd
SampleAdd inherits from **CoreModify**, and is used to specific a specific class for adding a new instance of an entity.  

This class must have a constructor and implement **GetId**, which contains the parts that indicate this is for adding a new item.  

## SampleEdit
SampleEdit inherits from **CoreModify**, and is used to specific a specific class for modifying an existing instance of an entity.  

This class must have a constructor and implement **GetId**, which contains the parts that indicate this is for modifying an existing item.  

## SampleDelete
SampleEdit inherits from **CoreDeleteAction**, which is used to delete an entity.  
**CoreDeleteAction** performs a lot of the mundane tasks to delete the entity, and is suitable for many of the more straight forward use cases.  
For more control, consider using **DoSomething** instead, as is demonstrated by **AdvancedDelete**.  

This class has the following methods and fields that need to be implemented:  

### EntityName
This returns the name of the entity and is used in input screen titles and dialog boxes.  

### ViewNumber
This is the even number of the view to display after a record is deleted.  

### GetId
This method should return a MenuNumber/EventNumber that uniquely identifies this view.
Each action/event/menu item in QBic has a unique menu or event number that is used to link an action with the code to execute.