# Register
Software Design Document
.NET Financial register + Calendar

Data -> A calendar innately must store dates and possibly times. Additionally, a string as a description or title of an event 
or a debit/credit to the register (if applicable). From what I have seen in other applications, XML is useful for the storing of
arbitrary data in related chunks. In this way I could have a series of <Event> tags that store <Date>, <Time> (and underneath time a <Start> and an optional <End>),
and <Description> information, as well as <Debit> or <Credit> info. For the most part, the two halves of this application (the calendar and the 
financial register) would need to see the information in the XML, making it more-or-less public to everything. Everything below
assumes the use of XML and XML parsing for this application, but another alternative is something like SQLite, which would mimic a 
server database (like SQL) to store and retrieve that information.

Archictecture -> The output of this program is the display of the calendar and the register, as well as any computations that 
are required to make sense out of it (e.g., the running total of the register, or a total of unpaid charges that the calendar 
is keeping track of). The user inputs a number of different things:
    -> One-off events to Calendar: All events must have <Date>s, and most will have <Time>s. These are one-off events, like 
    holiday plans or other special events.
    -> Recurring events to Calendar: These events happen regularly or at regular intervals. This would be something like a school
    or regular work schedule.
    -> One-off chargest to Register: Payments & charges to a register that are unplanned or infrequent enough that they must be 
    added regularly. 
    -> Recurring charges to Register: This is bills and subscriptions, especially ones that are exactly the same each month. 
    They can display on the calendar as well as in the register, but will have two states - paid & unpaid.
As stated above, the main component that will keep track of my runtime data will be XML, settings in a .bat file, visual display through
.NET, and C# for those sweet, sweet computations.

User Interface -> In theory the user could switch between 3 displays: Calendar only, mixed, and register only, with mixed being the default. 
Also, more complex processes could be accessable in the calendar only and register only sections, with the simpler ones being shown in 
the mixed form (or the mixed form could be read-only).
*include .imgs elsewhere

Procedural -> On startup the application would need to locate and load in the runtime files from this user. This would immedately 
populate the register and calendar with whatever has been stored there already. Then, the user can use the app's functions to sort 
information, get "reports", or add new information that is saved into the database and then updated in the app.
