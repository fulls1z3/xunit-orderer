using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace XunitOrderer {
    public class TestCollectionOrderer : ITestCollectionOrderer {
        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections) {
            var sortedCollections = new SortedDictionary<int, List<ITestCollection>>();

            foreach (var testCollection in testCollections) {
                var priority = 0;

                var index = testCollection.DisplayName.LastIndexOf(' ');

                if (index > -1) {
                    var assemblyName = testCollection.TestAssembly.Assembly.ToString().Split(',')[0];

                    var className = testCollection.DisplayName.Substring(index + 1);
                    var type = Type.GetType($"{className}, {assemblyName}");

                    if (type != null) {
                        var attr = type.GetCustomAttribute<TestPriorityAttribute>();
                        priority = attr?.Priority ?? 0;
                    }
                }

                GetOrCreate(sortedCollections, priority).Add(testCollection);
            }

            var r = new List<ITestCollection>();

            foreach (var list in sortedCollections.Keys.Select(priority => sortedCollections[priority])) {
                list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.CollectionDefinition.Name,
                    y.CollectionDefinition.Name));

                r.AddRange(list);
            }

            return r;
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new() {
            if (dictionary.TryGetValue(key, out var result))
                return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}
