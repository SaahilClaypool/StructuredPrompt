# StructuredPrompt

Inspired by <https://github.com/hwchase17/langchain>

See [program.cs](./example/Program.cs) for an example.

Ouptut:

Prompt:

    Generate one example.
    The output should be a markdown code snippet formatted in the following schema:
    ```json
    { 
            "A": Int32,
            "B": Int32?,
            "Date": DateOnly? // Before Jan 2020,
            "Inner": { 
            "InnerInt": Int32
    } // Inner class
    }
    ```

Response:

    ```json
    { 
            "A": 25,
            "B": null,
            "Date": "2019-12-15",
            "Inner": { 
                    "InnerInt": 10
            } 
    }
    ```

Parsed:

```json
{"A":25,"B":null,"Date":"2019-12-15","Inner":{"InnerInt":10}}
```
