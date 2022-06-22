# University Diploma Project
==============================

This project contains the implementation described in [Improving the Chatbot Experience: With a Content-based Recommender System](https://www.diva-portal.org/smash/get/diva2:1324846/FULLTEXT01.pdf), which I carried out in the spring semester of 2019 as part of my independent thesis basic level (university diploma) degree project at Mid Sweden University.

## Abstract

> Chatbots are computer programmes that are able to hold a conversation with a human user. When a chatbot is unable to match a user's utterance to a predefined response, it uses a fallback intent, i.e. a generic response that does not contribute meaningfully to the conversation. The purpose of this report is to investigate whether a content-based recommender system can support a chatbot agent in the case of these fallback experiences. Content-based recommender systems use content to filter, prioritise and deliver relevant information to the user. Their purpose is to search through a large amount of content and predict recommendations based on user needs. The recommendation system developed in this project consists of four components: a web spider, a bag-of-words model, a graph database and the GraphQL API. The expectation was to capture webpage articles and score them numerically to find out which articles make the best recommendations on specific topics. The chatbot agent could then use these recommended articles to provide value and help to the user instead of a generic answer. After evaluation, it was found that the recommendation system in principle fulfilled all the requirements, but that the scoring algorithm used could significantly improve its recommendations if a more advanced algorithm was implemented. The scoring algorithm used in this project is based on word count, which, among other things, does not take into account the context of the dialogue between the user and the agent.

## Usage

...

## Project Organization

...

## Note 

This repository is a hands-on part of a thesis project, and is therefore short-lived without long-term maintenance.

## Citation

If you use anything from this repository in a scientific publication, we would appreciate citations:

```
@misc{
    title   = {Improving the Chatbot Experience: With a Content-based Recommender System},
    author  = {Angelica Hjelm Gardner},
    url = {https://github.com/angelicagardner/chatbot-fallback-experience},
    date = {2022}
}
```