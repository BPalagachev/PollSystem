<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostNewQuestion.aspx.cs" Inherits="PollSystem.Web.PostNewQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table runat="server" id="TableInsertNewQuestion">
        <tr>
            <td>Enter your question:</td>
            <td>
                <asp:TextBox runat="server"
                             ID="TextBoxQueryText"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                
                <asp:GridView ID="GridViewAnswers" ItemType="PollSystem.Web.DataTransferObjects.AnswerByPostQuestionDto"
                    runat="server"
                    AutoGenerateColumns="False"
                    OnRowCommand="GridViewAnswers_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Answer Text:">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="TextBoxAnswerText" Text="<%#: Item.Text %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField
                            ButtonType="Image" 
                            CommandName="DeleteAnswer"
                            
                            HeaderText="Delete"
                            ImageUrl="~/Content/DeleteIcon32.png" Text="Button" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="ButtonAddNewRow" Text="Add new row" OnClick="ButtonAddNewRow_Click"/>
            </td>
        </tr>
    </table>
    <div>
        <asp:Button runat="server" ID="ButtonSubmitQuestion" Text="Submit Question" OnClick="ButtonSubmitQuestion_Click" OnClientClick="return confirm('Are you sure you?');" />
    </div>
</asp:Content>
