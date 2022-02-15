## Developer Blog
### Current Version: __1.1.0__
### Description:
Small program intended to act as a proto-structural backbone (i.e. back-end) for an eventual DevBlog.
As the main intended feature here is that of the functionality of such a program, the use of a UI has been foregone, in exchange for greater emphasis on logic structure and data integrity.

### Features:
- Methods for creating Authors and Posts 
- Storing this data temporarily in local "Storage"
- Transfering this locally stored data to (imagined) external database storage.
  - Often these two are done simultaneously, so that one acts as a data-integrity support, while the other (local) storage facilitates better theoretical response times.

### Deprecated Features:
- For the sake of transparency;
  - ID Handler class (responsibility relinquished to SQL database)
  - Exceptions, for the moment being, these Exceptions are no longer being employed.
  - CRUD Interface to enforce data integritry across classes intended to act as manageable objects in the program (Authors, Posts)
    - Deprecated to IStorage interface dur to SQL DB integration, as any CRUD performed on a live instance of the program, would have to be mirrored in the Storage class anyway.
      - By limiting this functionality to the former, responsibility is placed in a single place, while the Author and Post classes mostly function as scaffolding;
        I.e., they handle [C]reation of records, but inherently involve Storage, while Storage is most capable of accessing the permanent data storage (SQL DB) for reading, updates, and deletions/erasures.

### Update notes:
- 1.1.0
  - Finetuned certain methods throughout
  - Added unittest in order to validate certain commands and executions.

- 1.0.2
  - First pass for documentation added.
  - Slight modifications and clean-up in regards to CRUD methods and their implementation and functionality in terms of volatile and permanent storage options.
    - IStorage interface deprecated as a result.
    - Reposity for AuthorCRUD and PostCRUD established as a result.
    - Collection of "Common" classes moved to program specific folder, albeit it remains independent from it.
  - Added a few more helpers for Menus and Database methodologies.
  - Added simple UML (UML.png) to program, as Class Diagram functionality was shoddy - at best!

- 1.0.1
  - Expanded README for better granularity and clarity.
  - Added examples of async/await, extended methods, delegations, lambda, and SQL data management.
    - Experimented with more direct SQL storage (XML streaming), but this has been put on hold, as the gain of such a system were deemed outside the intended scope of the program.
    - If time allows, will look into this again, but for now, it's just good ol' "read the columns and create the objects using those data points", and vice verca.

- 1.0.0
  - Update reflects breaking changes to entire project the usurpes previous logic and structure:
    - Exceptions, IDHandler, and ICRUD have been deprecated
      - SQL Database integrated, which handles IDs.
      - ICRUD deemed inappropriate due to variations in creation methodologies.
        - As well as overlap with intended useage of the Storage and Database classes.

- 0.2.0
  - Added Common classes for Exception handling, ID Handling
  - Added Interfaces for CRUD and Storage
  - Began populating methods for Storage related functionality.
  - Began populating documentation for various methods throughout the program.

- 0.1.0
  - Started UML, Created classes: Author, Post, and Storage.


- 0.0.0
  - Initial commit.


### TO-DO:
- Play-time.
