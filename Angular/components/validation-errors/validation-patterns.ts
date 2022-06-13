export const VALIDATION_PATTERNS_DICTIONARY = {
  email: /^[a-z0-9+][a-z0-9+.-]+@[a-z]+\.[a-z]{2,3}$/,
  first_last_name: /^([A-Z][a-zA-Z' ]*)$/,
  twitter_link: /http(?:s)?:\/\/(?:www\.)?twitter\.com\/([a-zA-Z0-9_]+)/,
  facebook_link: /http(?:s)?:\/\/(?:www\.)?facebook\.com\/([a-zA-Z0-9_]+)/,
  linkedIn_link: /http(?:s)?:\/\/(?:www\.)?linkedin\.com\/([a-zA-Z0-9_]+)/,
  // eslint-disable-next-line no-useless-escape
  link: /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/
};
