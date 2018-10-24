# Starfinder Database for Unity

Open these files in a scene and add `WeaponDatabase.sh` to an object. Upon running, the script should be populated with data from each of the weapons in the JSON files.

## Requirements

JSON.net for Unity
https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347

## Functionality

#### start()

After attaching this script to a GameObject, `start()` will take effect on run. The function will check to see whether this GameObject already has values populated in the `MeleeWeaponsList` and `RangedWeaponsList`. If not, the values will be populated from the JSON files specified in `meleeCategories` and `rangedCategories`.

```
public void Start()
{
    // Weapon data is only loaded on first instantiation of Prefabs
    if (MeleeWeapons.Count == 0)
    {
        LoadWeaponData(meleeCategories, MeleeWeapons);
        MeleeWeaponsList = MeleeWeapons;
    }
    if (RangedWeapons.Count == 0)
    {
        LoadWeaponData(rangedCategories, RangedWeapons);
        RangedWeaponsList = RangedWeapons;
    }
    AllWeaponsList = MeleeWeaponsList.Union(RangedWeaponsList).ToList();
}
```

#### Categories

Categories are defined at the top of the WeaponsDatabase class, and are used on `start()`. The categories are simple strings, which are used for the following:

1. Specify the name of the JSON file. This will be passed as the `file_path` in the method `Database.GetJsonFromFile(file_path)`, which will concatenate it to create the full file path: `JsonHome + file_path + ".json")`
1. Assist in the parsing of JSON. The object created by `Newtonsoft.Json.Linq.JObject.Parse` requires a top-level JSON object to hold the subsequent entries. Each JSON file will have a top level item that shares the name of the JSON file.
1. Provide a category for searching. `WeaponDatabase.AddWeapon` takes a parameter called `title`, which will used to populate the field `Weapon.Category`. This field is subsequently used by `WeaponDatabase.SearchWeaponsByCategory` in order to filter `AllWeaponsList`.

#### JSON

The `json/` directory is used for adding data to Assets. The JSON files are handled by `JSON.net`. Should you choose to modify the location of the JSON files, simply modify `Database.JsonHome` accordingly.

> **Debugging Tip:**
> If an error occurs due to a JSON entry, run the script
> and look at which item is the last entered to the db.
> The next item on the list caused the error.
