# FakeAPI
## 簡介
以 C# 和 MSSQL 仿造 JSONPlaceholder API，目前內容僅包含對Post的基本CRUD。

## 使用簡介
1. 取得所有Post：  
    ```
    Method: Get
    Url:    https://{hostname}/api/v1.0/posts

    // Output
    {
        "IsSucceed": true,
        "ErrorMsg": null,
        "Data": [
            {
                "Id": 1,
                "Title": "Test",
                "Body": "This is a book.",
                "CreatedOn": "2020-07-09T01:25:30.163",
                "ModifiedOn": "2020-07-09T01:25:30.163"
            }
        ]
    }
    ```
2. 取得特定Post：  
    ```
    Method: Get
    Url:    https://{hostname}/api/v1.0/posts/{id}

    // Output
    {
        "IsSucceed": true,
        "ErrorMsg": null,
        "Data": {
            "Id": 1,
            "Title": "Test",
            "Body": "This is a book.",
            "CreatedOn": "2020-07-09T01:25:30.163",
            "ModifiedOn": "2020-07-09T01:25:30.163"
        }
    }
    ```
3. 新增Post：  
    ```
    Method: Post
    Url:    https://{hostname}/api/v1.0/posts

    // Body
    {
        "Id":2,
        "Title":"Test2",
        "Body":"Body2"
    }

    // Output
    {
        "IsSucceed": true,
        "ErrorMsg": null,
        "Data": {
            "Id": 2,
            "Title": "Test2",
            "Body": "Body2",
            "CreatedOn": "2020-07-09T21:45:06.3",
            "ModifiedOn": "2020-07-09T21:45:06.3"
        }
    }
    ```
 4. 修改特定Post：  
    ```
    Method: Put
    Url:    https://{hostname}/api/v1.0/posts/{id}

    // Body
    {
        "Id":2,
        "Title":"foo",
        "Body":"bar"
    }

    // Output
    {
        "IsSucceed": true,
        "ErrorMsg": null,
        "Data": {
            "Id": 2,
            "Title": "foo",
            "Body": "bar",
            "CreatedOn": "2020-07-09T21:45:06.3",
            "ModifiedOn": "2020-07-09T21:46:11.433"
        }
    }
    ```
5. 刪除特定Post
    ```
    Method: Delete
    Url:    https://{hostname}/api/v1.0/posts/{id}

    // Output
    {}
    ```