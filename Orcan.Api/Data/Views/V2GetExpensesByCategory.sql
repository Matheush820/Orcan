CREATE OR ALTER VIEW [V2GetExpensesByCategory] AS
SELECT 
    [Transaction].[UserId],
    [Category].[Title] AS [Category],
    YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
    SUM([Transaction].[Amount]) AS [Expenses]
FROM 
    [Transaction]
INNER JOIN [Category] ON 
    [Transaction].[CategoryId] = [Category].[Id]
WHERE 
    [Transaction].[PaidOrReceivedAt] >= DATEADD(MONTH, -1, CAST(GETDATE() AS DATE)) AND
    [Transaction].[PaidOrReceivedAt] < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
GROUP BY 
    [Transaction].[UserId],
    [Category].[Title],
    YEAR([Transaction].[PaidOrReceivedAt]);
