# User Management

The user administration control panel contains all the tools you need to manage user accounts. You can create user accounts, which are essentially user name/password pairs used to log on to the web site, and roles, which grant access to resources such as photos. 


##Built-in Roles
* The built-in Administrator role can perform any action on the web site, including managing content. 
* The built-in AuthorizedUser role can view restricted content.

A user can belong to more than one role. If the user belongs to no roles, the user has no privileges above and beyond an anonymous (not logged-in) user. Note that a user with no roles cannot view protected content even when logged in; that user must first be added to the AuthorizedUser role.


##Custom Roles
You can create additional roles for organizational purposes, but they have no functional effect at this time unless additional functionality is hard-coded into the web application. 


##Creating a User
TODO


##Password Reset
In the event a user loses access to his or her account, an Administrator can reset that user's password. 
1. Click on the user to reset
2. On the User Details page, click the Reset Password button. 
3. The user's password is reset and the user receives an e-mail with his or her new password. A comment is  also placed on the user account containing the new password. This comment should be deleted as soon as possible to preserve the security of the password. 

Note that passwords are not stored in plain-text. They are hashed and, as such, it is impossible to view or recover the current password. 

