Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace PrintCrosshairSample

    Public Class SaleItemRepository

        Private ReadOnly Shared companies As List(Of [String]) = New List(Of [String]) From {"DevAV North", "DevAV South", "DevAV West", "DevAV East", "DevAV Central"}

        Private ReadOnly Shared categorizedProducts As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String)) From {{"Cameras", New List(Of String)() From {"Camera", "Camcorder", "Binoculars", "Flash", "Tripod"}}, {"Cell Phones", New List(Of String)() From {"Smartphone", "Mobile Phone", "Smart Watch", "Sim Card"}}, {"Computers", New List(Of String)() From {"Desktop", "Laptop", "Tablet", "Printer"}}, {"TV, Audio", New List(Of String)() From {"Television", "Home Audio", "Headphone", "DVD Player"}}, {"Vehicle Electronics", New List(Of String)() From {"GPS Unit", "Radar", "Car Alarm", "Car Accessories"}}}

        Public Function GetProductsByMonths() As List(Of SaleItem)
            Dim rnd As Random = New Random()
            Dim items As List(Of SaleItem) = New List(Of SaleItem)()
            For Each company As String In companies
                For Each product As String In categorizedProducts("Cameras")
                    Dim dateTime As DateTime = New DateTime(2017, 12, 01)
                    For i As Integer = 0 To 12 - 1
                        Dim income As Integer = rnd.[Next](20, 100)
                        Dim revenue As Integer = income + rnd.[Next](20, 50)
                        items.Add(New SaleItem() With {company, product, .Month = dateTime.AddMonths(1).ToString("MMMM", CultureInfo.InvariantCulture), income, revenue})
                        dateTime = dateTime.AddMonths(1)
                    Next
                Next
            Next

            Return items
        End Function

        Public Function GetProductsByCompany(ByVal companyIndex As Integer) As List(Of SaleItem)
            Dim rnd As Random = New Random(DateTime.Now.Millisecond)
            Dim items As List(Of SaleItem) = New List(Of SaleItem)()
            For Each category As String In categorizedProducts.Keys
                For Each product As String In categorizedProducts(category)
                    Dim income As Integer = rnd.[Next](20, 100)
                    Dim revenue As Integer = income + rnd.[Next](20, 50)
                    items.Add(New SaleItem() With {.Company = companies(companyIndex), product, income, revenue, category})
                Next
            Next

            Return items
        End Function

        Public Function GetProductsIncome() As List(Of SaleItem)
            Dim rnd As Random = New Random(DateTime.Now.Millisecond)
            Dim items As List(Of SaleItem) = New List(Of SaleItem)()
            For i As Integer = 0 To 50 - 1
                For Each product As String In categorizedProducts("Cameras")
                    items.Add(New SaleItem() With {product, .Income = rnd.[Next](20, 100)})
                Next
            Next

            Return items
        End Function

        Public Function GetTotalIncome() As List(Of SaleItem)
            Dim rnd As Random = New Random(DateTime.Now.Millisecond)
            Dim now As DateTime = DateTime.Now
            Dim endDate As DateTime = New DateTime(now.Year, now.Month, 1)
            Dim items As List(Of SaleItem) = New List(Of SaleItem)()
            For Each company As String In companies
                Dim companyFactor As Double = rnd.NextDouble() * 0.6 + 1
                For Each category As String In categorizedProducts.Keys
                    Dim categoryFactor As Double = rnd.NextDouble() * 0.6 + 1
                    For Each product As String In categorizedProducts(category)
                        Dim maxIncome As Integer = rnd.[Next](60, 140)
                        For i As Integer = 0 To 1000 - 1
                            If i Mod 100 Is 0 Then maxIncome = Math.Max(40, rnd.[Next](maxIncome - 20, maxIncome + 20))
                            Dim [date] As DateTime = endDate.AddDays(-i - 1)
                            Dim income As Double = rnd.[Next](20, maxIncome) * companyFactor * categoryFactor
                            items.Add(New SaleItem() With {category, company, product, .OrderDate = [date], income})
                        Next
                    Next
                Next
            Next

            Return items
        End Function
    End Class

    Public Class SaleItem

        Public Property Product As String

        Public Property Company As String

        Public Property OrderDate As DateTime

        Public Property Month As String

        Public Property Income As Double

        Public Property Revenue As Double

        Public Property Category As String
    End Class
End Namespace
