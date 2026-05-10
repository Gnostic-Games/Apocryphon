using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

#nullable enable

namespace Apocryphon.Localization
{
    public static class LocalizationHandler
    {
        static readonly Dictionary<string, StringTable> tables = new();
        
        public static string GetEntry(string table, string id) => GetEntry(table, id, null);

        public static string GetEntry(string table, string id, params object[]? args)
        {
            StringTable? stringTable = GetTable(table);

            if (stringTable == null)
            {
                Debug.LogWarning($"Missing table {table}");

                return $"Missing table {table}";
            }

            string? localization = stringTable.GetEntry(id)?.GetLocalizedString(args);

            if (localization is null)
            {
                Debug.LogWarning($"Missing localization entry {id}");

                return $"Missing entry {id}";
            }

            return localization;
        }

        private static StringTable? GetTable(string id)
        {
            if (LocalizationSettings.SelectedLocale == null)
            {
                Debug.LogWarning("Selected locale is null");

                return null;
            }

            if(
                !tables.ContainsKey(id) ||
                tables[id].LocaleIdentifier != LocalizationSettings.SelectedLocale.LocaleName
            )
            {
                // if either we haven't loaded the table yet, or if the
                // language has changed since we loaded it, reload it
                StringTable? table = GetTableFromDB(id);

                if (table == null)
                {
                    Debug.LogWarning($"No table with identifier {id}");
                    return null;
                }

                tables[id] = table;
            }

            return tables[id];
        }

        public static bool HasEntry(string table, string id)
        {
            StringTable? stringTable = GetTable(table);

            if (stringTable == null)
            {
                Debug.LogWarning("Missing string table, returning false for HasEntry");

                return false;
            }

            return stringTable.GetEntry(id) != null;
        }

        private static StringTable? GetTableFromDB(string id)
        {
            return LocalizationSettings.StringDatabase.GetTable(id);
        }
    }
}