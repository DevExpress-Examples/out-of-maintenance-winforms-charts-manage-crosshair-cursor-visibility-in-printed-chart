Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace PrintCrosshairSample

    Public Class MainViewModel

        Private repository As PrintCrosshairSample.SaleItemRepository

        Public ReadOnly Property TotalIncome As IReadOnlyList(Of PrintCrosshairSample.SaleItemAggregate)
            Get
                Return Me.repository.GetTotalIncome().GroupBy(Function(i) New With {i.Category, i.Company}).[Select](Function(g) New PrintCrosshairSample.SaleItemAggregate With {.Category = g.Key.Category, .Company = g.Key.Company, .Value = g.Sum(Function(i) i.Income)}).ToList()
            End Get
        End Property

        Public Sub New()
            Me.repository = New PrintCrosshairSample.SaleItemRepository()
        End Sub
    End Class

    Public Class SaleItemAggregate

        Public Property Company As String

        Public Property Category As String

        Public Property Value As Double
    End Class
End Namespace
