# Get Start

1. Install next.js
```
$npx create-next-app .
```

2. Start project
```
$docker-compose up -d
$npm run dev
```

# Nextjs

## [Custom Document](https://nextjs.org/docs/advanced-features/custom-document)

## Strategy define style for each page
```
project
└───styles
│   │   golbales.css
│   │   Home.module.css
│   │   Another.module.css
│   
└───pages
    │   index.js <-- include Home.module.css
    │   another.js


<index.js>
import styles from '../styles/Home.module.css'

<another.js>
import styles from '../styles/Another.module.css'

```

