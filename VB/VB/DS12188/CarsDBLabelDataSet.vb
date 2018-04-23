' Developer Express Code Central Example:
' How to obtain labels from a datasource and handle deleted labels
' 
' This example elaborates the approach illustrated in example
' http://www.devexpress.com/scid=E2028. Labels for appointments are obtained from
' a data source. When deleting, the label is not actually removed but its color
' and caption are changed to identify a "deleted label" entity which should be
' treated specifically. An appointment labeled with the "deleted label" could not
' be created or edited (moved, resized).
' Additionally, this example demonstrates
' how to hide "deleted" labels when editing or creating a new appointment, as well
' as hide them in the appointment context menu.
' This approach can help you to
' implement more complex scenario.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2087

Imports Microsoft.VisualBasic
Imports System
Namespace DS12188


	Partial Public Class CarsDBLabelDataSet
		Partial Private Class LabelsDataTable
		End Class
	End Class
End Namespace
