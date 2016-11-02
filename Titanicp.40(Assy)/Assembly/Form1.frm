VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Form1"
   ClientHeight    =   1860
   ClientLeft      =   4470
   ClientTop       =   3630
   ClientWidth     =   3030
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1860
   ScaleWidth      =   3030
   Begin VB.CommandButton Command1 
      Caption         =   "Create Titanic"
      Height          =   615
      Left            =   960
      TabIndex        =   0
      Top             =   360
      Width           =   1335
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim CATIA As Object

Private Sub Command1_Click()

On Error Resume Next
Set CATIA = GetObject(, "CATIA.Application")
If Err.Number <> 0 Then
  Set CATIA = CreateObject("CATIA.Application")
  CATIA.Visible = True
End If
On Error GoTo 0


End Sub
