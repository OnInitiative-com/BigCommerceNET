# BigCommerceNET
The following project is based on a generic Nuget package library (BigCommerceAccess), which was deprecated for unknown reasons.

The current project refactors such library, using the latest .NET 8.0, and best standards for accessing WebAPIs (HttpClient, etc.) internally.

The Test Project shows a hint at how to use the library and some of the many Models that can be accessed from the BigCommerce store.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

BigCommerceNET is a .NET library that provides a convenient way to interact with the BigCommerce API. It simplifies the process of integrating your .NET applications with BigCommerce, allowing you to manage products, orders, customers, and more.

## Features

- **Easy Integration:** Quickly integrate your .NET applications with the BigCommerce API.
- **CRUD Operations:** Perform CRUD (Create, Read, Update, Delete) operations on products, orders, customers, and other resources.
- **Authentication:** Support for OAuth 2.0, V2, and V3 API connectivity authentication to ensure secure communication with the BigCommerce API.

## Getting Started

Follow these steps to get started with BigCommerceNET:

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-username/BigCommerceNET.git
2. **Install Dependencies:**
   Install package BigCommerceNET from the Nuget repository.
3. **Configure API Credentials:**
   Obtain your BigCommerce API credentials and update the configuration file with your client ID, client secret, and other relevant details.

## Usage

Here's a quick example of how to use BigCommerceNET in your .NET application:

```csharp
// Initialize the BigCommerce client
this.ConfigV3 = new BigCommerceConfig(ShortShopName, ClientId, ClientSecret, AccessToken);

// Retrieve a list of products
 var service = this.BigCommerceFactory.CreateProductsService(this.ConfigV3);

 var products = await service.GetProductsAsync(CancellationToken.None, true);

// Retrieve a list of categories
 var service = this.BigCommerceFactory.CreateCategoriesService(this.ConfigV3);

 List<BigCommerceCategory> categories = service.GetCategories();

// Perform other operations as needed
// ...
```

## Contributing

We welcome contributions! If you find any issues, have feature requests, or want to contribute in any other way, please open an issue or submit a pull request.

## License
This project is licensed under the [BSD-3-Clause license](https://github.com/OnInitiative-com/BigCommerceNET/blob/master/license.txt)




