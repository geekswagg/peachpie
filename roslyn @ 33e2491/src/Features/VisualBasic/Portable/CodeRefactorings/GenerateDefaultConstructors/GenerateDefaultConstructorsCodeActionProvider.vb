' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Composition
Imports Microsoft.CodeAnalysis.CodeRefactorings
Imports Microsoft.CodeAnalysis.GenerateMember.GenerateDefaultConstructors

Namespace Microsoft.CodeAnalysis.VisualBasic.CodeRefactorings.GenerateDefaultConstructors
    <ExportCodeRefactoringProvider(LanguageNames.VisualBasic, Name:=PredefinedCodeRefactoringProviderNames.GenerateDefaultConstructors), [Shared]>
    Friend Class GenerateDefaultConstructorsCodeRefactoringProvider
        Inherits CodeRefactoringProvider

        Public Overrides Async Function ComputeRefactoringsAsync(context As CodeRefactoringContext) As Task
            Dim document = context.Document
            Dim textSpan = context.Span
            Dim cancellationToken = context.CancellationToken

            Dim workspace = document.Project.Solution.Workspace
            If workspace.Kind = WorkspaceKind.MiscellaneousFiles Then
                Return
            End If

            Dim service = document.GetLanguageService(Of IGenerateDefaultConstructorsService)()
            Dim result = Await service.GenerateDefaultConstructorsAsync(document, textSpan, cancellationToken).ConfigureAwait(False)

            If Not result.ContainsChanges Then
                Return
            End If

            Dim actions = result.GetCodeRefactoring(cancellationToken).Actions
            context.RegisterRefactorings(actions)
        End Function
    End Class
End Namespace
