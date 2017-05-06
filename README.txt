* La base de données se crée dynamiquement. Si vous souhaitez la supprimer 
pour tester, j'ai mis à votre disposition un model "/SampleData" permettant
de la peupler avec des valeurs définies afin de ne pas avoir à recréer un 
environnement de test par vous même.
Utilisateur lambda : Paul / Paul
Administrateur : Admin / Admin

* Liste des fonctionnalités implémentées (en suivant le plan du sujet):
	- Connection / Register System
	- Account management :
		- Edit profile & password
	- Event management :
		- Create / Edit / Delete user Event
		- Event with "pending" status 
		- Particular types (users can create new types)
	- Friend management :
		- Find friend on the website by name or nickname
		- List of users ordered alphabetically
	- Admin page :
		- CRUD on :
			- Users
			- Events
			- Event types
			- Event Statuses
			- Contributions
			- Contribution types
			- Roles
		- Changing event statuses
		- Managing user passwords
		- Assign roles to users and manage available roles
