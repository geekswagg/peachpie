﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Editor.Implementation.InlineRename;
using Microsoft.CodeAnalysis.Host;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.CodeAnalysis.Editor
{
    /// <summary>
    /// This interface contains the methods required to manipulate the undo stack
    /// in each buffer during an inline rename session.  VS and ETA have differing
    /// implementations
    /// </summary>
    internal interface IInlineRenameUndoManager : IWorkspaceService
    {
        void CreateInitialState(string replacementText, ITextSelection selection, SnapshotSpan startingSpan);

        void CreateStartRenameUndoTransaction(Workspace workspace, ITextBuffer subjectBuffer, InlineRenameSession inlineRenameSession);
        void CreateConflictResolutionUndoTransaction(ITextBuffer subjectBuffer, Action applyEdit);

        void UndoTemporaryEdits(ITextBuffer subjectBuffer, bool disconnect);
        void OnTextChanged(ITextSelection selection, SnapshotSpan singleTrackingSpanTouched);

        void UpdateSelection(ITextView textView, ITextBuffer subjectBuffer, ITrackingSpan trackingSpan);
        void ApplyCurrentState(ITextBuffer subjectBuffer, object propagateSpansEditTag, IEnumerable<ITrackingSpan> spans);

        void Undo(ITextBuffer subjectBuffer);
        void Redo(ITextBuffer subjectBuffer);

        void Disconnect();
    }
}
