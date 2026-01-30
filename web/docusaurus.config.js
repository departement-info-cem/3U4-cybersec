// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion
import { themes } from 'prism-react-renderer';
import remarkMath from 'remark-math';
import rehypeKatex from 'rehype-katex';

const siteConfig = require("./config");

const lightCodeTheme = themes.vsLight
const darkCodeTheme = themes.vsDark

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: siteConfig.nom,
  tagline: siteConfig.description,
  url: "https://info.cegepmontpetit.ca/",
  baseUrl: `/${siteConfig.nomUrl}/`,
  onBrokenLinks: "warn",

  favicon: "img/favicon.ico",

  organizationName: "departement-info-cem",
  projectName: siteConfig.nomUrl,
  deploymentBranch: "gh-pages",
  trailingSlash: false,

  i18n: {
    defaultLocale: "fr",
    locales: ["fr"],
  },

  markdown: {
    mermaid: true,
    hooks: {
      onBrokenMarkdownLinks: "warn",
    },
  },
  themes: ['@docusaurus/theme-mermaid'],

  presets: [
    [
      "classic",
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve("./sidebars.js"),
          routeBasePath: "/",
          remarkPlugins: [remarkMath],
          rehypePlugins: [rehypeKatex],
          //editUrl: `https://github.com/departement-info-cem/${siteConfig.nomUrl}/tree/main/web`,
        },
        theme: {
          customCss: [require.resolve("./src/css/custom.css")],
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      docs: {
        sidebar: {
          hideable: true,
        },
      },
      navbar: {
        title: siteConfig.nom,
        logo: {
          alt: "Logo CEM",
          src: "img/logo.svg",
        },
        items: [
          {
            type: "doc",
            docId: "cours/r1.1",
            position: "left",
            label: "Cours",
          },
          {
            type: "docSidebar",
            position: "left",
            sidebarId: "tp",
            label: "Travaux Pratiques",
          },
          {
            position: "right",
            label: "Calendrier",
            href: "https://www.cegepmontpetit.ca/cegep/calendrier-scolaire",
          },
          {
            position: "right",
            label: "Repo",
            href: `https://github.com/departement-info-cem/${siteConfig.nomUrl}`,
          }
        ],
      },
      footer: {
        style: "dark",
        links: [
          {
            title: "Sources",
            items: [
              {
                label: "GitHub",
                href: `https://github.com/departement-info-cem/${siteConfig.nomUrl}`,
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()}. ${
          siteConfig.nom
        }. CÉGEP Édouard-Montpetit.`,
      },
      // Décommenter et remplir pour activer l'indexation des pages par le moteur de recherche local
      // algolia: {
      //   appId: '',
      //   apiKey: '',
      //   indexName: '',
      //   contextualSearch: true,
      //   searchPagePath: 'search',
      // },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
        additionalLanguages: ["csharp", "java", "dart", "powershell", "bash", "markdown", "csv", "json", "yaml"],
      },
      metadata: [
        {
          name: "keywords",
          content: `${siteConfig.nom}, ${siteConfig.description}, informatique, technique, cégep, cegep, édouard-montpetit, edouard-montpetit, édouard montpetit, edouard montpetit`,
        },
      ],
    }),
    stylesheets: [
      {
        href: 'https://cdn.jsdelivr.net/npm/katex@0.13.24/dist/katex.min.css',
        type: 'text/css',
        integrity:
          'sha384-odtC+0UGzzFL/6PNoE8rX/SPcQDXBJ+uRepguP4QkPCm2LBxH3FA3y+fKSiJ+AmM',
        crossorigin: 'anonymous',
      },
    ],
};

module.exports = config;
