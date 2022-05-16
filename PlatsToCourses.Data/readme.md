# Pour générer le code à partir d'une table existante
Scaffold-DbContext "Server=SVD1SQL-STM\SQL2014;Database=BDDPlatsToCourses;Trusted_Connection=Yes;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -project PlatsToCourses.Data -Tables "T_Setting" -Context MyContext

# Ajouter une migration
add-migration NomMigration -project PlatsToCourses.Data -context BDDPlatsToCoursesContext

# Jouer les migrations
update-database -context BDDPlatsToCoursesContext