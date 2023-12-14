<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionGroupForm.aspx.cs" Inherits="MainProject.QuestionGroupForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
   <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />

<script>
    $(document).ready(function () {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 2000,
            "extendedTimeOut": 1000
        };

    });
</script>

</head>
<body>
    <script>

        function RowDoubleClicked(s, e) {
            var rowNumber = s.GetRowKey(e.visibleIndex)
            cp.PerformCallback("RowDoubleClicked|" + rowNumber);
            ASPxClientEdit.ClearGroup("QuestionGroup");
        }

        function ButtonReset_clicked(s, e) {

            cp.PerformCallback("ButtonReset_clicked");
        }


        function ButtonSave_clicked(s, e) {
            if (ASPxClientEdit.ValidateEditorsInContainerById("ASPxFormLayout3", "QuestionGroup", true)) {
                cp.PerformCallback("ButtonSave_clicked");
                toastr.success("Data Saved Successfully", "Success");

            }
        }
        function buttonBack_clicked(s, e) {

            cp.PerformCallback("buttonBack_clicked");


        }
        function OnCustomButtonClick(s, e) {

            var rowNumber = s.GetRowKey(e.visibleIndex)

            cp.PerformCallback("OnCustomButtonClick|" + rowNumber);


            var buttonID = e.buttonID;

            if (buttonID === "customDelete") {
                Swal.fire({
                    title: "Are you sure?",
                    text: "You won't be able to revert this!",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    confirmButtonText: "Yes, delete it!"
                }).then((result) => {
                    if (result.isConfirmed) {
                        cp.PerformCallback("OnCustomDeleteButtonClick|" + rowNumber);

                        Swal.fire({
                            title: "Deleted!",
                            text: "this record has been deleted.",
                            icon: "success"
                        });
                    }
                });
            }
        }


    </script>
    <form id="form1" runat="server">
        <div>

            <div style="padding: 10px;">
                <a href="Default.aspx" style="font-size: 30px; text-decoration: none;"><strong>APEX</strong>  Local Hospital System</a>
            </div>

            <dx:ASPxCallbackPanel ID="cp" runat="server" ClientInstanceName="cp" OnCallback="cp_Callback">

                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">

                        <dx:ASPxFormLayout ID="ASPxFormLayout3" CssClass="centered-layout" runat="server" BackColor="White">
                            <Items>
                                <dx:LayoutGroup Caption="Group Setup" Width="97%" ColCount="2" ColSpan="1" ColumnCount="2" UseDefaultPaddings="false" Paddings-PaddingTop="10" Name="SearchGroup">
                                    <GridSettings StretchLastItem="true" WrapCaptionAtWidth="1200">
                                        <Breakpoints>
                                            <dx:LayoutBreakpoint MaxWidth="900" ColumnCount="1" Name="M" />
                                        </Breakpoints>
                                    </GridSettings>
                                    <Paddings PaddingTop="10px"></Paddings>
                                    <Items>

                                        <dx:LayoutItem Caption="" CaptionSettings-Location="Top" HorizontalAlign="Right" ColSpan="2">

                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">

                                                    <dx:ASPxButton ID="buttonBack" RenderMode="Outline" runat="server" Width="200px" ForeColor="#333333" CssClass="control_inline"
                                                        VerticalAlign="Top" Height="5%" Enabled="true" BackColor="#f2f3f2" Border-BorderStyle="Solid"
                                                        Border-BorderWidth="1px" Border-BorderColor="#cccccc" Text="Back To Groups">
                                                        <Paddings Padding="4px"></Paddings>
                                                        <ClientSideEvents Click="buttonBack_clicked" />
                                                    </dx:ASPxButton>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="Question" CaptionSettings-Location="Top" ColSpan="1">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxComboBox ID="selectQuestions" Width="100%" runat="server">
                                                        <ValidationSettings ValidationGroup="QuestionGroup" ErrorTextPosition="Bottom" ValidateOnLeave="False">
                                                            <RequiredField IsRequired="True" ErrorText="Field is required" />
                                                        </ValidationSettings>
                                                        <Items>
                                                        </Items>

                                                    </dx:ASPxComboBox>

                                                    <asp:HiddenField runat="server" ID="HiddenOnUpdate" />
                                                    <asp:HiddenField runat="server" ID="hiddenQuestionGroupId" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="Order" CaptionSettings-Location="Top" ColSpan="1">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1"
                                                    BreakpointName="M"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxSpinEdit ID="spinQuestionGroupOrder" runat="server" Number="1" LargeIncrement="1">
                                                        <ValidationSettings ValidationGroup="QuestionGroup" ErrorTextPosition="Bottom" ValidateOnLeave="False">
                                                            <RequiredField IsRequired="True" ErrorText="Field is required" />
                                                        </ValidationSettings>
                                                        <SpinButtons ShowIncrementButtons="True" ShowLargeIncrementButtons="false" />
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="" CaptionSettings-Location="Top" HorizontalAlign="Right" ColSpan="2">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">

                                                    <dx:ASPxButton ID="ButtonSave" AutoPostBack="false" ValidationGroup="QuestionGroup" RenderMode="Outline" runat="server" Width="200px" ForeColor="#333333" CssClass="control_inline"
                                                        VerticalAlign="Top" Height="5%" Enabled="true" BackColor="#f2f3f2" Border-BorderStyle="Solid"
                                                        Border-BorderWidth="1px" Border-BorderColor="#cccccc" Text="Save">
                                                        <Paddings Padding="4px"></Paddings>
                                                        <ClientSideEvents Click="ButtonSave_clicked" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton ID="ButtonReset" RenderMode="Outline" AutoPostBack="false" runat="server" Width="200px" ForeColor="#333333" CssClass="control_inline"
                                                        VerticalAlign="Top" Height="5%" Enabled="true" BackColor="#f2f3f2" Border-BorderStyle="Solid"
                                                        Border-BorderWidth="1px" Border-BorderColor="#cccccc" Text="Reset">
                                                        <Paddings Padding="4px"></Paddings>
                                                        <ClientSideEvents Click="ButtonReset_clicked" />

                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="" ColSpan="2" ColumnSpan="2">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxGridView ID="gridGroupQuestions" KeyFieldName="ID" runat="server">
                                                        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
                                                        <SettingsResizing ColumnResizeMode="NextColumn" />

                                                        <SettingsAdaptivity>

                                                            <AdaptiveDetailLayoutProperties SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit">
                                                            </AdaptiveDetailLayoutProperties>
                                                        </SettingsAdaptivity>
                                                        <SettingsPopup>
                                                        </SettingsPopup>
                                                        <Paddings PaddingTop="50px" />
                                                        <Columns>

                                                            <dx:GridViewCommandColumn Caption=" " VisibleIndex="0">
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="customDelete" Image-Width="20px" Image-Height="20px" Image-Url="~/images/close.png" Text=" ">
                                                                        <Image Height="20px" Width="20px" Url="~/images/close.png"></Image>
                                                                    </dx:GridViewCommandColumnCustomButton>

                                                                </CustomButtons>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="QuestionText" Caption="Question Text">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="LOVType" Caption="LOV Type">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="AnswerObject" Caption="Answer Object">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="AnswerAttribute" Caption="Answer Attribute">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="IsFreeText" Caption="Is Free Text">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="AnswerYesValueCriteria" Caption="Answer Yes Value Criteria">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CREATION_DATETIME" Caption="CREATION DATETIME">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CREATED_BY" Caption="CREATED BY">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="UPDATE_DATETIME" Caption="UPDATE DATETIME">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="UPDATED_BY" Caption="UPDATED BY">
                                                            </dx:GridViewDataTextColumn>

                                                        </Columns>

                                                        <SettingsBehavior AllowSelectByRowClick="true" />

                                                        <ClientSideEvents RowDblClick="RowDoubleClicked" />


                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>








                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>
    </form>
</body>
</html>
