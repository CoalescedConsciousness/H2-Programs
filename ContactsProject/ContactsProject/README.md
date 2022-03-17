### Assignment: Contactlist
### Author: Mads Søndergaard
### Version: 1.1.0

### Changelog:
- 1.1.0
  - Reconfigued storage logic to utilize custom made SQL database, rather than the one originally created via Entity Framework functionality.
	- Note that, for now, this new SQL logic is non-generic; if time permits, I'll attempt to reconfigure it further, and refactor it into generic logic.
	- Currently, the Database consists of the following Stored Procedures:
		- ContactCreate: Creates a Contact
		- ContactDelete: Deletes a Contact on first pass, Erases it on the second.
		- ContactWrite: Edits a given column of an existing Contact
		- ContactRestore: Restores a deleted (but not erased!) contact.
		- GetContactByID: Fetches a record based on its unique ID
		- ContactGetAll: Fetches every Contact record
	- Note that the database does not utilize IDENTITY_INSERT, to avoid confounding pre-existing ID's with new ones, despite allowing for record erasure
		- The reasoning here, is that to ensure data integrity, and adherence to GDRP legislation, the risk of exposing data to unwanted parties due to ID's only being unique transiently, must be reduced.
			- This was deemed a necessary precaution, despite the somewhat more messier datastructure in the database itself.
			- ..Deal with it kids, M'kay?

- 1.0.0
  - Program considered presentation-ready.
    - Ability to Create, Edit, Delete, and Read "Contacts"
	- Ability to sort by Favourites, as well as mark as favourites.
	- Includes validation of input.

- 0.0.0
  - Initial Commit




## Models:
- Contact
  - Property: Name (obligatory)
  - Property: List<Contactmethod> (Length >= 1)

- Contactmethod (abstract)
  - Infofield
  - ValidateInfoField() (?)
	
  - Contactmethod[A..X] (child)



## SRS:
1. Contactlist of contacts (people)
   - Name (obligatory)
   - Contact-method 1
   - Contact-method 2 (at least)
   - ... x

2. Front-page > List of all Contacts
   - Toggle individual record as "Favourite"
   - "Save" > Refresh list

3. Front-page > Function to create new Contactlist

4. Front-page > Contactlist > Record-specific button ==> Edit contactperson
   - Validate name (not empty)
   - Mark as favourite during edit
   - DateTime of change

5. Front-page > Menuitem > "Overview of Favourites"
   - Listview showing only contact + 1 contact-method (telephone, email, etc.)

6. Validate userinput, fittign errormessages.

7. Generic Errorpage rather than costum.

(8. Graphical styling)