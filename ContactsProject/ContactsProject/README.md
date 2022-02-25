### Assignment: Contactlist
### Author: Mads Søndergaard
### Version: 0.0.0

### Changelog:





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