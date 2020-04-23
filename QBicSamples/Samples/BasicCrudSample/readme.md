# Introduction
The **CategoryCrudMenuItem** inherits from **BasicCrudMenuItem**, which is the simplest way to create a view of information, with the ability to add and modify new records as well as delete records.  

Implementing an instance of **BasicCrudMenuItem** requires a specific generic type, which is the class that will be used to update the database.  

When creating an instance of **BasicCrudMenuItem**, the QBic application will automatically attempt to perform many of the actions related to displaying data, adding data, modifying and deleting data.  
A lot of this is done by means of reflection, and as a result, this class is mostly used for relatively simple classes.  

Example use cases for using **BasicCrudMenuItem** is to easily create lists that will appear as inputs on other screens relating to other classes, and which can be maintained by the users themselves.  
Instead of hard coding many values as an Enum or possible using List<T>, this is another option to allow more dynamic behaviour within your application.

When creating a **BasicCrudMenuItem** class, the following methods should generally be implemented:

### AllowInMenu
Setting this value to true, will a menu item to be added which will show this view when clicked. If unimplemented, this value defaults to **false**.  
For BasicCrudMenuItem, this should be set to true, in most cases.  

### UniquePropertyName
This is an optional override, and this can be used to ensure records in the database have a specific value be unique.  

### GetBaseItemName
This is used to set the name of the entity being worked with and will be used to create the various titles on the various screens.

### GetBaseMenuId
This method should return a MenuNumber/EventNumber that uniquely identifies basic crud item.  
Each action/event/menu item in QBic has a unique menu or event number that is used to link an action with the code to execute.  
For the BasicCrudMenuItem, additional Event Number are automatically generated from the value returned by this result. 
The automatically generated values will be this value plus 1, and plus 2, which are used for modify and delete functions, respectively.

### GetColumnsToShowInView
This method returns the list of columns that should be displayed in the screen view for this entity.  
The result is a dictionary where the key value needs to match one of the property names of the entity, and the value is the title of the column displayed to the user, and can be anything.

### GetInputProperties
This method returns the list of inputs that will be used when a user adds or modifies an instance of this entity.  
The result is a dictionary where the key value needs to match one of the property names of the entity, and the value is the label that will be displayed on the input screen when performing and add or modify.
