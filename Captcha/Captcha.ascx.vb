Imports System.Text

Partial Class Controls_Captcha
    Inherits System.Web.UI.UserControl



    Private _pwdFieldID As String
    Public Property PasswordFieldID() As String
        Get
            Return _pwdFieldID
        End Get
        Set(ByVal value As String)
            _pwdFieldID = value
        End Set
    End Property



    'Determines no of characters to generate in the captcha code.
    Private _NumOfChars As Integer = 6
    Public Property NumberOfChars() As Integer
        Get
            Return _NumOfChars
        End Get
        Set(ByVal value As Integer)
            If value >= 1 Then
                _NumOfChars = value
            Else
                _NumOfChars = 6
            End If
        End Set
    End Property

    'Determines whether captcha code is case-sensitive or case-insensitive.
    Private _CaseSensitiveCode As Boolean = False
    Public Property CaseSensitiveCode() As Boolean
        Get
            Return _CaseSensitiveCode
        End Get
        Set(ByVal value As Boolean)
            _CaseSensitiveCode = value
        End Set
    End Property

    'When evaluated, checks whether the captcha entered by user is valid or invalid.
    Public ReadOnly Property IsValid() As Boolean
        Get
            'validate captcha
            'return valid/invalid flag
            Return ValidateCaptcha()
        End Get
    End Property

    'Decides whether to show a border around the captcha control.
    Private _ShowBorder As Boolean = False
    Public Property ShowBorder() As Boolean
        Set(ByVal value As Boolean)
            If value = True Then
                CaptchaDiv.Style.Add("border", "solid 1px #ccc")
            Else
                CaptchaDiv.Style.Remove("border")
            End If
        End Set
        Get
            Return _ShowBorder
        End Get
    End Property


    Private _validationGroup As String
    Public Property ValidationGroup() As String
        Get
            Return _validationGroup
        End Get
        Set(ByVal value As String)
            rvfCaptchaCode.ValidationGroup = value
            _validationGroup = value
        End Set
    End Property


    'Helper method that validates the captcha control. Used by IsValid property.
    Private Function ValidateCaptcha() As Boolean
        'obtain captcha code from the session.
        Dim storedCaptchaCode As String = Session("CaptchaCode")

        'for case sensitive code
        If CaseSensitiveCode Then
            Return txtCaptchaCodeInput.Text.Trim() = storedCaptchaCode
        End If

        'for non-case sensitive code
        Return txtCaptchaCodeInput.Text.Trim().ToLower() = storedCaptchaCode.ToLower()
    End Function

    'Creates a new random captcha code, store it in session and generates image for it.
    Public Sub Generate()
        Session("CaptchaCode") = GenerateCode()
        imgCaptcha.ImageUrl = "~/Captcha/GetCaptchaCodedImage.ashx"
    End Sub

    'Helper method that generates captcha code of set length.
    Private Function GenerateCode() As String
        'Characters used for generating captcha code.
        Dim charsMap() As String = New String() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", _
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", _
        "1", "2", "3", "4", "5", "6", "7", "8", "9"}

        Dim rnd As New Random()
        Dim sb As New StringBuilder("")
        Dim i As Integer
        For i = 1 To NumberOfChars
            sb.Append(charsMap(rnd.Next(charsMap.Length)))
        Next

        Return sb.ToString()
    End Function

    'Refreshes the captcha code.
    Protected Sub btnReGenerate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnReGenerate.Click
        Clear()
        'Re-create the captcha code
        Generate()
    End Sub

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Generate captcha intially when requested first time on a page.
        If Not Page.IsPostBack Then
            Generate()
            rvfCaptchaCode.ValidationGroup = Me.ValidationGroup

            'txtCaptchaCodeInput.Focus()
        End If
    End Sub

    'Sets focus on the input text box for captcha control.
    Public Overrides Sub Focus()
        txtCaptchaCodeInput.Focus()
    End Sub

    'Clears the input text box for captcha control.
    Public Sub Clear()
        txtCaptchaCodeInput.Text = ""
        'txtCaptchaCodeInput.Focus()
    End Sub

End Class
