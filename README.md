# RPG Database for Unity

This is a simple database builder for use in RPG style games. 

The following README contains development notes for reference.

## Requirements

JSON.net for Unity
https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347

## Functionality

### start()

After attaching this script to a GameObject, `start()` will take effect on run. The function will check to see whether this GameObject already has values populated in the appropriate list(s), such as `MeleeWeaponsList` and `RangedWeaponsList`. If not, the values will be populated from the JSON files specified in the associated category (such as `meleeCategories` and `rangedCategories`).

### Categories

Categories are defined at the top of the WeaponsDatabase class, and are used on `start()`. The categories are simple strings, which are used for the following:

1. Specify the name of the JSON file. This will be passed as the `file_path` in the method `Database.GetJsonFromFile(file_path)`, which will concatenate it to create the full file path: `JsonHome + file_path + ".json")`
1. Assist in the parsing of JSON. The object created by `Newtonsoft.Json.Linq.JObject.Parse` requires a top-level JSON object to hold the subsequent entries. Each JSON file will have a top level item that shares the name of the JSON file.
1. Provide a category for searching. `WeaponDatabase.AddWeapon` takes a parameter called `title`, which will used to populate the field `Weapon.Category`. This field is subsequently used by `WeaponDatabase.SearchWeaponsByCategory` in order to filter `AllWeaponsList`.

#### JSON

The `/json` directory is used for housing data files. The JSON files are handled by `JSON.net`. Should you choose to modify the location of the JSON files, simply modify `Database.JsonHome` accordingly.

> **Debugging Tip:**
> If an error occurs due to a JSON entry, run the script
> and look at which item is the last entered to the db.
> The next item on the list caused the error.

### Search

There is currently one primary search function that can be used in the code: 

`Database.GetByName()`

This returns an object of the type that is specific to the database, such as `class Weapon`

**Example Search:**
```
 Weapon debugWeapon = FindWeaponByName("Bow");
 PropertyInfo[] properties = typeof(Weapon).GetProperties();
 foreach (PropertyInfo property in properties)
 {
    Debug.Log(property.Name + ": " + property.GetValue(debugWeapon, null));
 }
```

## Optional Modifications

### JsonHome
```
Relative location of directory containing JSON files
public static string JsonHome = @"json/";
```
In the default case 'MyProject/json" lives beside "MyProject/Assets", so the relative path is "json".

### Categories
```
public static string[] meleeCategories = { "1h_melee", "2h_melee" };
public static string[] rangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };
```
If the JSON file names are modified, or if additional files are added, be sure to modify the top-level object name *and* the appropriate categories list(s) in the associated class object. This enables the data to be properly loaded.

## TODO

### Searching
Search functionality will need dramatic improvement. Currently, the only reliable search function across all categories is `GetByName()`. WeaponDatabase has starters for other search types that should be refined before being implemented elsewhere. 

Also, __the search functions are all currently case-sensitive__.
