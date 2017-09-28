# EducationEfMvc Project

This is a project to do both MVC and WebAPI.

I fixed the relationship between Student and Major and the collections they use by adding the [JsonIgnore] on the relationship property in the Major table. Thus, when the Major table instance is added to the Student instance, the Students collection will not be filled for the Major instance. 