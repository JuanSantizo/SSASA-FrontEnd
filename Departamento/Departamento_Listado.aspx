<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departamento_Listado.aspx.cs" Inherits="FrameWork.Departamento.Departamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    
    <div class="container">
        <div class="row">
            <div class="col-6">                
                 <asp:Label ID="lblTitulo" runat="server" CssClass="fs-4 fw-bold">Registro de Departamentos</asp:Label>
            </div>
            <div class="col-6">
                <asp:LinkButton  runat="server" OnClick="Nuevo_Click" 
                    CssClass="btn btn-light" ><img src="../image/agregar.png" /> Nuevo</asp:LinkButton>                
            </div>
        </div>


        <div class="row" style="padding:4px 0px 0px 0px">
            <div class="col-12">

                <asp:GridView ID="GVDepartamento" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">

                    <Columns>
                        <asp:BoundField DataField="DepartamentoId" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Departamento" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" CommandArgument='<%# Eval("DepartamentoId") %>'
                                    OnClick="Editar_Click" CssClass="btn btn-light"> <img src="../image/editar.png" /> Editar</asp:LinkButton>

                                <asp:LinkButton runat="server" CommandArgument='<%# Eval("DepartamentoId") %>'
                                    OnClick="Eliminar_Click" CssClass="btn btn-light" 
                                    OnClientClick="return confirm('Desea eliminar?')"><img src="../image/eliminar.png" /> Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
