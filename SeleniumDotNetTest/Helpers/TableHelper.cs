using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class TableHelper
    {
        public List<string> ReturnEvenOrOddValuesFromList(int oddOrEven, List<string> stringList)
        {
            List<string> newListEven = new List<string>();
            List<string> newListOdd = new List<string>();
            List<string> returnList = new List<string>();

            for (int indexList = 0; indexList < stringList.Count; indexList++)
            {
                if (indexList % 2 == 0)
                {
                    newListEven.Add(stringList[indexList]);
                }
                else
                {
                    newListOdd.Add(stringList[indexList]);
                }
            }

            switch (oddOrEven)
            {
                case 0:
                    returnList.AddRange(newListEven);
                    break;
                case 1:
                    returnList.AddRange(newListOdd);
                    break;
            }

            return returnList;
        }

        public List<int> ReturnIndexFromTableHeaders(IWebElement tableElement, List<string> stringListColumnHeader)
        {
            IList<IWebElement> tableHeaders = tableElement.FindElements(By.TagName("th"));
            List<int> indexColumns = new List<int>();
            for (int currentColumn = 0; currentColumn < tableHeaders.Count; currentColumn++)
            {
                IWebElement tableHeaderElement = tableHeaders[currentColumn];
                string tableHeaderText = tableHeaderElement.Text;
                for (int currentColumnToFind = 0; currentColumnToFind < stringListColumnHeader.Count; currentColumnToFind++)
                {
                    string desireTableHeader = stringListColumnHeader[currentColumnToFind];
                    if (tableHeaderText == desireTableHeader)
                    {
                        indexColumns.Add(currentColumn);
                        break;
                    }
                }
            }
            return indexColumns;
        }

        public dynamic CountValuesOnTable(IWebElement tableElement, List<string> listTableColumnsAndValues, bool returnTableRowElement = false)
        {

            List<string> listColumnNames = ReturnEvenOrOddValuesFromList(0, listTableColumnsAndValues);
            List<string> listTableValues = ReturnEvenOrOddValuesFromList(1, listTableColumnsAndValues);
            List<int> listTableHeaderIndex = ReturnIndexFromTableHeaders(tableElement, listColumnNames);

            int numberRows = 0;
            // Get all the table rows elements;
            IList<IWebElement> tableRows = tableElement.FindElements(By.TagName("tr"));

            // Loop through all the table rows;
            IWebElement rowElement;
            for (int currentRow = 0; currentRow < tableRows.Count; currentRow++)
            {
                // Get the current row element and list of elements with table data (cells);
                rowElement = tableRows[currentRow];
                IList<IWebElement> listRowTD = rowElement.FindElements(By.TagName("td"));

                // Skip in case of the row element did not contain TD elements (for example, rows with TH elements);
                if (listRowTD.Count == 0)
                {
                    continue;
                }

                // Loop through the columns;
                bool found = false;
                for (int currentColumnIndex = 0; currentColumnIndex < listTableHeaderIndex.Count; currentColumnIndex++)
                {
                    int columnToInspect = listTableHeaderIndex[currentColumnIndex];
                    string tableCellValue = listRowTD[columnToInspect].Text;
                    string tableCellExpectedValue = listTableValues[currentColumnIndex];

                    // Break and keep the row loop;
                    if (tableCellValue != tableCellExpectedValue)
                    {
                        found = false;
                        break;
                    }

                    // Set found true in the value match;
                    found = true;
                }

                // Increment the number of rows with the expected value;
                if (found)
                {
                    numberRows++;
                    if (returnTableRowElement)
                    {
                        return rowElement;
                    }
                }

            }

            return numberRows;
        }

        public dynamic ReturnTableRowElement(IWebElement tableElement, List<string> listTableColumnsAndValues)
        {
            return CountValuesOnTable(tableElement, listTableColumnsAndValues, true);
        }
    }
}
