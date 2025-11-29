# Migration Guide

This repository has undergone a significant restructuring and quality upgrade.

## Changes

1.  **Repository Clean**: Removed accidental commit of `obj/` folder artifacts from the root.
2.  **Project Structure**:
    - Source code moved to `src/PlainTextEditor`.
    - Tests moved to `tests/PlainTextEditor.Tests`.
    - Solution file created at root: `PlainTextEditor.sln`.
3.  **Project Renaming**:
    - Renamed from inferred `management` (found in artifacts) to `PlainTextEditor`.
4.  **Framework**:
    - Targeted `.NET 8` (Windows).
5.  **CI/CD**:
    - Added GitHub Actions workflow (`.github/workflows/ci.yml`) for automated building and testing.

## How to Migrate

If you have local changes or are pulling this for the first time:

1.  `git fetch origin`
2.  `git checkout main` (or the relevant branch)
3.  Ensure you have .NET 8 SDK installed.
4.  Open `PlainTextEditor.sln` instead of looking for files in the root.
