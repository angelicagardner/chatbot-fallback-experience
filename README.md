# Improving the Chatbot Fallback Experience

This repository contains the reference implementation for [Improving the Chatbot Experience: With a Content-based Recommender System](https://www.diva-portal.org/smash/get/diva2:1324846/FULLTEXT01.pdf), an independent thesis basic level (university diploma).

> Note: This code was developed specifically for the thesis and is no longer maintained. The repository is archived.

## Changelog

[June 2019] – Thesis published on DiVA.

[Aug 2025] – Source code made public here.

## Problem statement

Chatbots often fall back to generic “I’m sorry, I don’t understand” replies whenever they can’t match user input to a scripted response. This leads to a poor user experience and missed opportunities for engagement. This project implements a content-based recommender system to enrich chatbot fallbacks. The aim is to deliver more informative, contextually relevant responses whenever the chatbot’s primary intent matcher fails.

## Solution overview

1. **Web Spider**: Uses DotnetSpider to crawl configured seed URLs, extract text, and emit JSON records.

2. **Bag-of-Words Model**: Normalizes and tokenizes scraped content, filters stop-words, and computes term frequencies.

3. **Graph Store**: Ingests articles, paragraphs, and keywords into Neo4j, modeling relationships and weighting occurrences.

4. **GraphQL API**: Receives fallback queries, traverses the graph to rank candidate passages, and returns the top recommendation for the chatbot.

## Getting started

### Prerequisites

- .NET SDK 7.0 or later
- Neo4j 4.x or later

### Installation

1. Clone this Git repository

2. Set the correct configurations:
    - Edit `src/ChatbotRecommender.Crawler/appsettings.json` to specify your seed URLs and storage options (e.g. file vs database).
    - In `src/ChatbotRecommender.Api/appsettings.json`, update the ConnectionStrings:Neo4j entry to point to your Neo4j instance (bolt URL, username, password).

3. Build & Run
```bash
dotnet restore
dotnet build
```

4. Run the crawler to scrape sites, process text into a bag-of-words model, and load data into your Neo4j graph:
```bash
dotnet run --project src/ChatbotRecommender.Crawler/ChatbotRecommender.Crawler.csproj
```

5. Check your Neo4j browser to confirm that Article, Paragraph, and Word nodes have been created.

6. Start the API
```bash
dotnet run --project src/ChatbotRecommender.Api/ChatbotRecommender.Api.csproj
```
By default it listens on `http://localhost:5000`. You can now POST fallback queries:
```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{"query":"{ recommend(query:\"chatbot fallback\") { url, snippet } }"}'
```

### Tests

Each component has its own test project. To execute all tests:
```bash
dotnet test
```

## Conclusion & Future Work

The prototype demonstrated that a simple word-count scoring approach can surface more relevant fallback replies than a static default. However improvements, such as embedding-based similarity, deeper conversational context, and real-time incremental updates, would further enhance recommendation quality and system performance.

## Citation

```
@misc{gardner2019improving,
  author        = {Angelica Hjelm Gardner},
  title         = {Improving the Chatbot Experience: With a Content-Based Recommender System},
  howpublished  = {University Diploma Project, Mid Sweden University},
  year          = {2019},
  url           = {https://github.com/angelicagardner/chatbot-fallback-experience}
}
```