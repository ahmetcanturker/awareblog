import { BlogPost } from '../contract/dto/BlogPost';
import { post as post1 } from './MockPost1';
import { post as post2 } from './MockPost2';

export const posts: BlogPost[] = [
    {
        date: new Date('2020-07-18'),
        title: 'Title of a longer featured blog post',
        summary: "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
        image: {
            url: 'https://source.unsplash.com/random',
            description: 'main image description'
        },
        contentMd: post1
    },
    {
        date: new Date('2020-07-18'),
        title: 'Featured post',
        summary:
            'This is a wider card with supporting text below as a natural lead-in to additional content.',
        image: {
            url: 'https://source.unsplash.com/random',
            description: 'Image Text'
        },
        contentMd: post2,
    },
    {
        date: new Date('2020-07-18'),
        title: 'Post title',
        summary: 'This is a wider card with supporting text below as a natural lead-in to additional content.',
        image: {
            url: 'https://source.unsplash.com/random',
            description: 'Image Text'
        },
        contentMd: post2
    }
];