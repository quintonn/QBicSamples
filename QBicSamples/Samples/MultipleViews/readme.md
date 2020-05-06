# Introduction
The **MultipleViews** folder contains code that demonstrates navigating a hierarchy of data.  

In this example, the primary view, which will be accessible from the menu, is "View Manufacturers".  
This can be used to display Vehicle Manufactureres, for example, such as Toyota, Ford, Hyundai, etc.  

From this main view, a user can select to view a specific manufacturer's models.  
This will then show a view of models. Models for Hyundai for example may be, Tucson, Elantra, ix35, and so on.  

From within the view of Models, a user can select to view a specific model's different editions.  
This will then show another view with the various editions of a specific model. This could include the different years or classes within the model.  
For example, for the Hyundai ix35, there could be editions Active X, Elite, Highlander, SE, Value, SEL, Sport, Limited.

On each of the subsequent views after the main view, there is a back button on the view menu.  
This back button allows returning to the previous view, while maintaining the context that view was displayed in.  
What this means is that if the user was viewing models for Toyota for example, and then chose to view the different editions for a specific model, they could then go back to view the models for Toyota.  

This functionality is possibly more complicated than it needs to be, and might be looked at in future, but for now, this folder demonstrates the code required to achieve this functionality.  

### DataForGettingMenu and GetEventParameters
Two different fields need to be populated with information in order to keep track of the current context, or in the example, which manufacturer we are viewing.

Data returned by **GetEventParameters** is what is available in **settings.ViewData** when the GetData and GetDataCount methods are invoked when a user performs a search or pages through the data.

Data returned by **DataForGettingMenu** is what is available in the **dataForMenu** parameter in the **GetViewMenu** function which is used to add view menu buttons.
