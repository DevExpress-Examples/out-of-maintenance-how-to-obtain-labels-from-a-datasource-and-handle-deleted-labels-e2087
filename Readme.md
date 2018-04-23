# How to obtain labels from a datasource and handle deleted labels


<p>This example elaborates the approach illustrated in example <a href="https://www.devexpress.com/Support/Center/p/E2028">Obtain labels from a datasource like the resources</a>. Labels for appointments are obtained from a data source. When deleting, the label is not actually removed but its color and caption are changed to identify a "deleted label" entity which should be treated specifically. An appointment labeled with the "deleted label" could not be created or edited (moved, resized).</p><p>Additionally, this example demonstrates how to hide "deleted" labels when editing or creating a new appointment, as well as hide them in the appointment context menu.</p><p>This approach can help you to implement more complex scenario.</p>

<br/>


