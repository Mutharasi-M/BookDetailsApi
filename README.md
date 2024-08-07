Book Details API
----------------

The Book Details API provides functionalities to manage and retrieve information about books,
including sorting, saving, and retrieving citations in MLA and Chicago styles.



Table of Contents
----------------

Overview

Prerequisites

Installation

Endpoints

Data Definitions

Stored Procedures

Citation Generation



Overview
--------

This API is built using .NET Core 8 and supports interaction with a SQL Server database via Entity Framework for data access. 

It provides endpoints to manage books, including sorting by publisher and author, calculating the total price of books, and saving a list of books to the database.



Prerequisites
-------------

Before running the API, ensure you have the following installed:

Microsoft.EntityFrameworkCore.SqlServer - 8

Microsoft.EntityFrameworkCore.Tools - 8

MSSQL Server - v19.1 - 2023

Visual Studio - 2022

Endpoints
---------

POST /api/BookDetailsApi/initialize-sample-data: Initializes sample book data if none exists.

POST /api/BookDetailsApi/save-books: Saves a list of books to the database.

PUT /api/BookDetailsApi/update-books: Updates a list of books in the database.

DELETE /api/BookDetailsApi/delete-books: Deletes books from the database by their IDs.

GET /api/BookDetailsApi/sorted-by-publisher: Retrieves books sorted by Publisher, Author, and Title.

GET /api/BookDetailsApi/sorted-by-author: Retrieves books sorted by Author and Title.

GET /api/BookDetailsApi/sorted-by-publisher-with-stored-procedure: Retrieves books sorted by Publisher using a stored procedure.

GET /api/BookDetailsApi/sorted-by-author-with-stored-procedure: Retrieves books sorted by Author using a stored procedure.

GET /api/BookDetailsApi/total-price: Retrieves the total price of all books in the database.


Data Definitions
----------------

BookDetail Model

csharp

public class BookDetail

{

    public int Id { get; set; }
    
    public string Publisher { get; set; }
    
    public string Title { get; set; }
    
    public string AuthorLastName { get; set; }
    
    public string AuthorFirstName { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public string? ContainerTitle { get; set; }
    
    public string? JournalTitle { get; set; }
    
    public int? VolumeNumber { get; set; }
    
    public string? PageRange { get; set; }
    
    public string? UrlOrDoi { get; set; }
    
    public string MLACitation { get; }
    
    public string ChicagoCitation { get; }
    

    // Methods for generating MLA and Chicago citations
    
    private string BuildMLACitation() { ... }
    
    private string BuildChicagoCitation() { ... }
    
}


Stored Procedures
------------------

GetBooksSortedByPublisher

sql

Copy code




CREATE PROCEDURE GetBooksSortedByPublisher

AS

BEGIN

    SELECT * FROM BookDetails
    
    ORDER BY Publisher, AuthorLastName, AuthorFirstName, Title
    
END


GetBooksSortedByAuthor


sql

Copy code



CREATE PROCEDURE GetBooksSortedByAuthor

AS

BEGIN

    SELECT * FROM BookDetails
    
    ORDER BY AuthorLastName, AuthorFirstName, Title
    
END


Citation Generation
-------------------

The MLACitation and ChicagoCitation properties in the BookDetail model generate citations in MLA and Chicago styles, respectively, based on the book details.
