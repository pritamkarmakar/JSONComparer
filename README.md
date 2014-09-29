-- If you are a Regex expert you can do it easily, but I'm not so I wrote this stuff.

### Why this library?
I was working in a test automation where I need to play with hundreds on JSON as those are my REST services output. Now there was a situation where I need to make sure my service is returning correct JSON schema with all parameters and with correct values. For that I kept a predefined JSON in my framework and the test case will retrieve the service response and will compare with the pre-existing content. But there was a problem, in those JSON strings few parameters were dynamic so I was not able to compare directly.

### Ok give me an example -

`{"ColumnData":{"DataNames":{}},"SourceId":"b473f1fd-45de-11e4-b55d-005056993566","Name":"My data source","Text":"SymmetricalData|Sheet1", "Description":"Description for query"}`

lets say above JSON is one of my service response. In the response 'SourceId' is dynamic and I want to avoid this parameter during comparison. Using this library we can do that.

There are two overloaded public methods 'ReplaceValues' -

`public string ReplaceValues(string json, string[] keys)` : send the parameters that you want to replace in the 'keys' array and it will replace all those parameters with empty guid.

`public string ReplaceValues(string json, string[] keys, string[] values)` : this method will replace the key value with the one that you will provide in the 'values' array. It will pick the same index value from the 'values' array, so make sure you keep the key in the 'keys' array and corresponding value in the 'values' array in same index.

### Sample output from the first method:
`{"ColumnMetaData":{"ColumnNames":{}},"DataSourceId":"00000000-0000-0000-0000-000000000000","Name":"My data source","Text":"SymmetricalData|Sheet1", "Description":"Description for query"}`

I'm not a geek.7
