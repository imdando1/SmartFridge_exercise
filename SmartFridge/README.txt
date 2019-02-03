- INTRODUCTION
	The purpose of this exercise is to implement a SmartFridge Manager that can perform various logic needed 
	for the SmartFridge.  Based on the SmartFridgeManager interface written Java, the actual implementation
	is written in c#.  Few changes were made in the original SmartFridge interface to clarify some confusions in 
	the semantics of the description.  Any changes made in the interface was provided a reason to why it was 
	changed in the actual comment.

- STRUCTURE
	It may have been an overkill for such a small exercise but repository pattern was used to not only separate 
	concerns, but also to mimic the connection to the database. 

	- DataModels
		Holds all data model classes such as FridgeItem.

	- DataService
		DataServices is the one that should be getting the data from the repository and then doing the heavy
		lifting of data manipulation before sending it to the end point. 
		FridgeItemsService (renamed from SmartFridgeManager) lives here. 

	- Repositories
		Providers that has direct connection to the data base.  In this exercise, since we don't have an actual
		database, it uses a singleton to act as a fake database.  Ideally, down the road (when using a real 
		database), a generic repository pattern can be used in order to consolidate all of the other repositories
		if it's ultimately doing the same thing but targeting a different data set.

	- EventHandlers
		This contains all event handling classes that may be used across the project.  Any event contracts are
		made in the EventConfig file. 

	- [SmartFridge.Test]
		This project contains unit tests for the project.  In this exercise, only the test for 
		FridgeItemsService (SmartFridgeManager) was completed. 
		Regardless of which library(and language) is used for the unit test, it is my strong personal opinion that 
		it should be organized in a way so that it follows the three "A"s of unit test which is 
		"Arrange", "Act", and "Assert".

- OTHER
	- Event handling is demonstrated in Program.cs.  
		




