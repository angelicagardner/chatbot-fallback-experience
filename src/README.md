# Source Code: Reference Implementation

> **Reconstruction Note (2026):** This source code is a reconstruction of the original 2019 thesis project. It illustrates the architecture and logic described in my project, following C# 7.3 syntax and .NET Core 2.2 patterns.

## ⚠️ Why this is a "Technical Showcase"

This repository serves best as a "read-only" reference for three primary reasons:

1. **Legacy Dependencies:** Built with `.NET Core 2.2` and `DotnetSpider 3.0`. These libraries are "End of Life" and require a legacy environment to compile.

2. **Environment Specifics:** Requires a live Neo4j instance and specific Dialogflow webhook configurations.

3. **Data Integrity:** The target website (`simplea.com`) structure has likely evolved since 2019, which would require updated XPath expressions in the `SimpleaDataParser`.

## 🧠 What this code demonstrates

Despite being a reconstruction, this code demonstrates:

- **ETL Pipeline Design:** A clear flow from Extract (Spider) → Transform (NLP/DataProcessor) → Load (Neo4j Ingestion).

- **Graph Modeling:** Moving beyond relational tables to model contextual relationships between articles and paragraphs.

- **API Architecture:** Using GraphQL to provide a flexible interface for conversational AI fallbacks.

## 🏗 Architectural Pattern: Shared Infrastructure

To maintain a DRY approach, the system utilizes a Shared Infrastructure Layer.

- **The Ingestion Pipeline** (`ABot.Spider`) and **The Query Service** (`ABot.RecommenderSystem`) both reference the same Infrastructure Core.

- This core contains the `Neo4jDriver`, which handles the low-level Bolt protocol communication, and the `DataTypes` (POCOs) that define our domain model (`Article`, `Paragraph`, `Word`).
