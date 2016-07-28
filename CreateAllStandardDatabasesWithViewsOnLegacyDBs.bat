

rem Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\ControlDProduction .\sqlexpress ControlDProduction DropAndCreateEverythingExceptViewsAndPermissions
pushd Databases\ControlDProduction\
cscript.exe CreateDatabase.vbs ControlDProduction W7D0632\SQLEXPRESS -v Views\order.txt
POPD

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\FieldMeasurement. .\sqlexpress FieldMeasurement DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\WindFarm .\sqlexpress WindFarm DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Project\Databases .\sqlexpress RESProjects DropAndCreateEverythingExceptViewsAndPermissions

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Project\Databases .\sqlexpress RESProjects CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\FieldMeasurement. .\sqlexpress FieldMeasurement CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Databases\WindFarm .\sqlexpress WindFarm DropAndCreateEverythingExceptViewsAndPermissions

rem At this point the three leagcy dbs are created with views (for the most part - a few controlDproduction ones are missing).

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe User\Databases .\sqlexpress User DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineModel\Databases .\sqlexpress TurbineModel DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineLayout\Databases .\sqlexpress TurbineLayout DropAndCreateEverythingExceptViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Energy\Gross\Databases .\sqlexpress GrossEnergy DropAndCreateEverythingExceptViewsAndPermissions

Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe User\Databases .\sqlexpress User CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineModel\Databases .\sqlexpress TurbineModel CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe TurbineLayout\Databases .\sqlexpress TurbineLayout CreateViewsAndPermissions
Applications\Absolution\CreateDatabase.Console\bin\Release\CreateDatabase.Console.exe Energy\Gross\Databases .\sqlexpress GrossEnergy CreateViewsAndPermissions