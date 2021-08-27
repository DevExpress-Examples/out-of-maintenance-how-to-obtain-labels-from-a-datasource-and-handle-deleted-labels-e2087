<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635687/15.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2087)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to obtain labels from a datasource and handle deleted labels


<p>This example elaborates the approach illustrated in example <a href="https://www.devexpress.com/Support/Center/p/E2028">Obtain labels from a datasource like the resources</a>. Labels for appointments are obtained from a data source. When deleting, the label is not actually removed but its color and caption are changed to identify a "deleted label" entity which should be treated specifically. An appointment labeled with the "deleted label" could not be created or edited (moved, resized).</p><p>Additionally, this example demonstrates how to hide "deleted" labels when editing or creating a new appointment, as well as hide them in the appointment context menu.</p><p>This approach can help you to implement more complex scenario.</p>

<br/>


