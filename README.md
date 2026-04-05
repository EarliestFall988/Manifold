# React-ASP Framework

I know ASP.NET has a template for react - this is not that. I built this framework from scratch, so it's clean and simple.

## React

I like react but I also come with **strong** opinions on how to use it.

These are the tools and libraries that I personally must have to build world class applications with react.

I have used all of these tools in production applications and have found them to be CRITICAL for building scalable and maintainable applications.

Not everyone agrees with me on these tools and that's ok.

### VITE

Vite handles all the building, bundling, and hot module replacement for the React application. It provides a fast development experience and optimizes the production build.

Basically it's all the plumbing I don't want to have to deal with. It also has a plugin system that allows us to easily integrate other tools and libraries (below).

Feel free to checkout `vite.config.ts` to see how it's configured.

### Tailwind CSS

I will fight to put this on any new project I work with. Tailwind handles all the styling for the application.

The best way to describe tailwind is zen mode CSS.

### Tanstack Libraries

I will rave about these two libraries until the end of time. They are the best in their respective categories and work together beautifully.

#### Tanstack Router

https://tanstack.com/router/latest

I use something called file based routing which maps the file structure to the URL structure.

This makes it really easy to locate issues in the code, even when you're new to the codebase.

#### Tanstack Query

https://tanstack.com/query/latest

Query handles the most frusterating part of any application, data fetching and caching. With exceptions - you don't need Redux, Zustand, or any other crazy state management library.


